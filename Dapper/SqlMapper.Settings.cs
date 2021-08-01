using System;
using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public static class Settings
        {
            private const CommandBehavior DefaultAllowedCommandBehaviors = ~CommandBehavior.SingleResult;
            internal static CommandBehavior AllowedCommandBehaviors { get; private set; } = DefaultAllowedCommandBehaviors;
            private static void SetAllowedCommandBehaviors(CommandBehavior behavior, bool enabled)
            {
                if (enabled) AllowedCommandBehaviors |= behavior;
                else AllowedCommandBehaviors &= ~behavior;
            }
            public static bool UseSingleResultOptimization
            {
                get { return (AllowedCommandBehaviors & CommandBehavior.SingleResult) != 0; }
                set { SetAllowedCommandBehaviors(CommandBehavior.SingleResult, value); }
            }
            public static bool UseSingleRowOptimization
            {
                get { return (AllowedCommandBehaviors & CommandBehavior.SingleRow) != 0; }
                set { SetAllowedCommandBehaviors(CommandBehavior.SingleRow, value); }
            }

            internal static bool DisableCommandBehaviorOptimizations(CommandBehavior behavior, Exception ex)
            {
                if (AllowedCommandBehaviors == DefaultAllowedCommandBehaviors
                    && (behavior & (CommandBehavior.SingleResult | CommandBehavior.SingleRow)) != 0)
                {
                    if (ex.Message.Contains(nameof(CommandBehavior.SingleResult))
                        || ex.Message.Contains(nameof(CommandBehavior.SingleRow)))
                    {
                        SetAllowedCommandBehaviors(CommandBehavior.SingleResult | CommandBehavior.SingleRow, false);
                        return true;
                    }
                }
                return false;
            }

            static Settings()
            {
                SetDefaults();
            }

            public static void SetDefaults()
            {
                CommandTimeout = null;
                ApplyNullValues = false;
            }

            public static int? CommandTimeout { get; set; }

            public static bool ApplyNullValues { get; set; }

            public static bool PadListExpansions { get; set; }
            public static int InListStringSplitCount { get; set; } = -1;
        }
    }
}
