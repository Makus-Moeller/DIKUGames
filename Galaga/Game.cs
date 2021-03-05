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
        private GameEventBus<object> eventBus;
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosion;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;

        public Game() {    
            window = new Window("Galaga", 500, 500);            
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))); 
            gameTimer = new GameTimer(30, 30);
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent}); 

            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f , 0.1f)), new ImageStride(80, images)));
            } 
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosion = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8, 
                Path.Combine("Assets", "Images", "Explosion.png"));
            
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    player.SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(true);
                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_LEFT":
                    player.SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(false);
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
                //TODO: move the shots shape
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    //TODO: Delet shot
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        //TODO: if collision btw shot and enemy -> delete both
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {  
                            enemy.DeleteEntity();
                            shot.DeleteEntity();
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        }
                    });
                }
            });
        }
        public void AddExplosion(Vec2F position, Vec2F extent) {
            //TODO: add explosion to the Animationcontainer 
            enemyExplosion.AddAnimation(new StationaryShape(position, extent), EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));


        }
        public void Run() {
            while(window.IsRunning()) {
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    eventBus.ProcessEvents();
                    player.Move();
                    IterateShots();
                }
                
                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    // render game entities here...
                    enemies.RenderEntities();
                    player.Render();
                    playerShots.RenderEntities();
                    enemyExplosion.RenderAnimations();
                    //SKAL altid være nederst
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