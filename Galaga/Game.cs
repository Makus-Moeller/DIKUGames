using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;

namespace Galaga {
    public class Game : IGameEventProcessor<object> {
        private GameEventBus<object> eventBus;
        private Player player;
        private Window window;
        private GameTimer gameTimer;
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


        public Game() {    
            window = new Window("Galaga", 500, 500);            
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))); 
            gameTimer = new GameTimer(30, 30);
            gameOverText = new Text("GAMEOVER \nYOU LOOSE", (new Vec2F(0.35f, 0.3f)), (new Vec2F(0.3f,0.3f)));
            gameOverText.SetColor(new Vec3I(192, 0, 255));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            //Events
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent}); 
            window.RegisterEventBus(eventBus);
            gameScore = new Score(new Vec2F(0.05f, 0.01f), new Vec2F(0.2f, 0.2f));
            
            //subscribing objects and eventtypes
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.GameStateEvent, gameScore);
            //Initialize enemy AllSquadrons
            CreateSquadrons();

            //EntityContainer og Grafiske objekter
            int numEnemies = 8;
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosion = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets", "Images", "Explosion.png"));


                   
        }
        //Laver squads både i constructor og når alle er døde
        public void CreateSquadrons () {
            List<ISquadron> tempList = new List<ISquadron>();
            var diagonale = new DiagonaleSquad(4, new ZigZagDown());
            var Vertical = new VerticaleSquad(4, new Down());
            var kavadrad = new KvadratiskSquad(4, new ZigZagDown());
            tempList.Add(diagonale); tempList.Add(Vertical); tempList.Add(kavadrad);
            AllSquadrons = tempList;
            foreach (ISquadron squad in AllSquadrons) {
                squad.CreateEnemies(images, enemyStridesRed);
                IncreaseDifficulty.IncreaseSpeedDown(squad.strat);
            }
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_RIGHT", "MoveRight", "1"));
                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_LEFT":
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_LEFT_RELEASED", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_RIGHT_RELEASED", "MoveRight", "1"));
                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    playerShots.AddEntity(new PlayerShot(new Vec2F(player.getPosiiton().X + (player.ExtentX / 2), player.getPosiiton().Y), playerShotImage));
                    break;
            }    
        }
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;
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
                            if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
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
            enemyExplosion.AddAnimation(new StationaryShape(position, extent), EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
            eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.GameStateEvent, this, "INCREASE_SCORE", "MoveRight", "1"));


        }
        public void Run() {
            while(window.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    eventBus.ProcessEvents();
                    player.Move();
                    IterateShots();

                    if (Enemy.TOTAL_ENEMIES == 0) {
                        CreateSquadrons();
                    }
                    foreach (ISquadron squad in AllSquadrons) {
                        squad.strat.MoveEnemies(squad.Enemies);
                    }
                }
                
                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    // render game entities here...
                    if (!IterateEnemy()) {
                        foreach (ISquadron squad in AllSquadrons) {
                            squad.Enemies.RenderEntities();
                        }
                        player.Render();
                        playerShots.RenderEntities();
                        enemyExplosion.RenderAnimations();
                        gameScore.RenderScore();
                    } else {
                        gameScore.RenderScore();
                        gameOverText.RenderText();
                    }

                    
                    //window always in the buttom 
                    window.SwapBuffers();
                    
                }

                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }            
    }
}