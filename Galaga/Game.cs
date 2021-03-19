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
        private EntityContainer<Enemy> enemies;
        private DiagonaleSquad diagonaleSquad;
        private VerticaleSquad verticaleSquad;
        private KvadratiskSquad kvadratiskSquad;
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

        public Game() {    
            window = new Window("Galaga", 500, 500);            
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))); 
            gameTimer = new GameTimer(30, 30);
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent}); 
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            window.RegisterEventBus(eventBus);
            gameScore = new Score(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.01f));
            
            //subscribing objects and eventtypes
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.GameStateEvent, gameScore);



            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f , 0.1f)), new ImageStride(80, images), new ImageStride(80, enemyStridesRed)));
            } 
            diagonaleSquad = new DiagonaleSquad(4);
            diagonaleSquad.CreateEnemies(images, enemyStridesRed);
            verticaleSquad = new VerticaleSquad(4);
            verticaleSquad.CreateEnemies(images, enemyStridesRed);
            kvadratiskSquad = new KvadratiskSquad(4);
            kvadratiskSquad.CreateEnemies(images, enemyStridesRed);
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosion = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets", "Images", "Explosion.png"));
            //New movePlayer funktion
                   
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    //player.SetMoveLeft(true);
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    //player.SetMoveRight(true);
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
                    //player.SetMoveLeft(false);
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_LEFT_RELEASED", "MoveLeft", "1"));
                    break;
                case "KEY_RIGHT":
                    //player.SetMoveRight(false);
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.PlayerEvent, this, "KEY_RIGHT_RELEASED", "MoveRight", "1"));
                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    playerShots.AddEntity(new PlayerShot(player.getPosiiton(), playerShotImage));
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

        private void IterateShots() {
            playerShots.Iterate(shot => {
                //move the shots shape
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    //Delete shot
                    shot.DeleteEntity();
                } else {
                    diagonaleSquad.Enemies.Iterate(enemy => {
                        //if collision btw shot and enemy -> delete both
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {  
                            if(enemy.Enrage()) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            }
                            shot.DeleteEntity();
                        }
                    }); 
                    verticaleSquad.Enemies.Iterate(enemy => {
                        //if collision btw shot and enemy -> delete both
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {  
                            if(enemy.Enrage()) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            }
                            shot.DeleteEntity();
                        }
                    });
                    kvadratiskSquad.Enemies.Iterate(enemy => {
                        //if collision btw shot and enemy -> delete both
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {  
                            if(enemy.Enrage()) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            }
                            shot.DeleteEntity();
                        }
                    });
                }
            });
        }
        public void AddExplosion(Vec2F position, Vec2F extent) {
            //add explosion to the Animationcontainer 
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
                    diagonaleSquad.strat.MoveEnemies(diagonaleSquad.Enemies);
                }
                
                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    // render game entities here...
                    diagonaleSquad.Enemies.RenderEntities();
                    verticaleSquad.Enemies.RenderEntities();
                    kvadratiskSquad.Enemies.RenderEntities();
                    player.Render();
                    playerShots.RenderEntities();
                    enemyExplosion.RenderAnimations();
                    //window always in the buttom 
                    window.SwapBuffers();
                    gameScore.RenderScore();
                }

                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }            
    }
}