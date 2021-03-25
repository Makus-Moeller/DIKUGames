using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.EventBus;
using GalagaStates;
using Galaga;
using System.Collections.Generic;
using Galaga.Squadrons;
using Galaga.MovementStrategy;
using DIKUArcade.Physics;
namespace GalagaStates {
    public class GameRunning : IGameState {
        private Player player;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosion;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private List<Image> enemyStridesRed;
        private Score gameScore;
        private List<Image> images;
        private List<ISquadron> AllSquadrons;
        private Text gameOverText {get;}
        private DiagonaleSquad diagonal;
        private KvadratiskSquad kvadratisk;
        private VerticalSquad vertical;
        
        
        
        
        private static GameRunning instance = null;
        public GameRunning() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))); 

            //Grafik
            gameOverText = new Text("GAMEOVER \nYOU LOOSE", (new Vec2F(0.35f, 0.3f)), 
                (new Vec2F(0.3f,0.3f)));
            gameOverText.SetColor(new Vec3I(192, 0, 255));
            enemyStridesRed = ImageStride.CreateStrides(
                2, Path.Combine("Assets", "Images", "RedMonster.png"));
            images = ImageStride.CreateStrides(
                4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            //Score
            gameScore = new Score(new Vec2F(0.05f, 0.01f), new Vec2F(0.2f, 0.2f));

            //EventBus
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
            GalagaBus.GetBus().Subscribe(GameEventType.StatusEvent, gameScore);
            
            

            //Enemies
            diagonal = new DiagonaleSquad(4, new ZigZagDown());
            vertical = new VerticalSquad(4, new Down());
            kvadratisk = new KvadratiskSquad(4, new ZigZagDown());
            AllSquadrons = new List<ISquadron>();
            AllSquadrons.Add(diagonal); AllSquadrons.Add(vertical); AllSquadrons.Add(kvadratisk);
            foreach (ISquadron squad in AllSquadrons) {
                squad.CreateEnemies(images, enemyStridesRed);
            }
            //Player and explosions
            int numEnemies = 8;
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosion = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets", "Images", "Explosion.png"));
        }

        public static GameRunning GetInstance() {
            
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
                case "KEY_RELEASE":
                    KeyRelease(keyValue);
                    break;
                case "KEY_PRESS":
                    KeyPress(keyValue);
                    break;
                default:
                    break;
            }
        }
        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_RIGHT", "MoveRight", "1"));
                    break;
                case "KEY_ESCAPE":
                    //TIL PAUSE
                    break;
                default:
                    break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_LEFT":
                    GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT_RELEASED", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_RIGHT_RELEASED", "MoveRight", "1"));
                    break;
                case "KEY_SPACE":
                    playerShots.AddEntity(new PlayerShot(
                        new Vec2F(player.getPosiiton().X + (player.ExtentX / 2), 
                        player.getPosiiton().Y), playerShotImage));
                    break;
                default:
                    break;
            }    
        }
        
        //Creates new enemies in AllSquadrons 
        public void CreateNewEnemiesInSquadrons() {
            foreach (ISquadron squad in AllSquadrons) {
                squad.CreateEnemies(images, enemyStridesRed);
                IncreaseDifficulty.IncreaseSpeedDown(squad.strat);
            }
        }
        private bool IterateEnemy() {
            bool hasLost = false;
            foreach (ISquadron squad in AllSquadrons) {
                squad.Enemies.Iterate(enemy => {
                    if (enemy.Shape.Position.Y < 0.2f) {
                        hasLost = true;
                    }
                });
            }
            return hasLost;
        }
        private void IterateShots() {
            playerShots.Iterate(shot => {
                //move the shots shape
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    //Delete shot
                    shot.DeleteEntity();
                } else {
                    foreach (ISquadron squad in AllSquadrons) {
                        squad.Enemies.Iterate(enemy => {

                            //if collision btw shot and enemy -> delete both
                            if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), 
                            enemy.Shape).Collision) {
                                //Tjekker om enemy skal dø  når den bliver ramt  
                                if(enemy.Enrage()) {
                                    AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                }
                                shot.DeleteEntity();
                            }
                        }); 
                    }
                }
            });
        }
        
        public void AddExplosion(Vec2F position, Vec2F extent) {
            //add explosion to the Animationcontainer
            //Event adds 1 to our score
            enemyExplosion.AddAnimation(new StationaryShape(position, extent), EXPLOSION_LENGTH_MS, 
                new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
        }
        
        public void GameLoop()
        {
            throw new NotImplementedException();
        } 
        public void InitializeGameState()
        {
            throw new NotImplementedException();
        }

        public void RenderState() {
            if (!IterateEnemy()) {
                foreach (ISquadron squad in AllSquadrons) {
                    squad.Enemies.RenderEntities();
                }
                player.Render();
                playerShots.RenderEntities();
                enemyExplosion.RenderAnimations();
                gameScore.RenderScore();
            } 
            else {
                gameScore.RenderScore();
                gameOverText.RenderText();
            }
        }

        public void UpdateGameLogic() { 
            player.Move();
            IterateShots();

            if (Enemy.TOTAL_ENEMIES == 0) {
                CreateNewEnemiesInSquadrons();
            }
            foreach (ISquadron squad in AllSquadrons) {
                squad.strat.MoveEnemies(squad.Enemies);
            }
        }
    }
}