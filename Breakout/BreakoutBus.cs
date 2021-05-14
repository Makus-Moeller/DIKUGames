using DIKUArcade.Events;

namespace Breakout {

    /// <summary>
    /// allows you to always acces eventBus
    /// </summary>
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus =
                new GameEventBus());
        }
    }
}