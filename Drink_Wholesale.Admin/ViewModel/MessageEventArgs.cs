using System;

namespace ELTE.TodoList.Desktop.ViewModel
{
    /// <summary>
    /// Üzenet eseményargumentum típusa.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Üzenet lekérdezése, vagy beállítása.
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// Üzenet eseményargumentum példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.</param>
        public MessageEventArgs(String message) { Message = message; }
    }

    public class YesNoMessageEventArgs
    {
        public String Message { get; set; }
        public String Yes { get; set; }
        public String No { get; set; }
        public String Cancel { get; set; }
        public YesNoMessageEventArgs(String message, String yesString = "Yes", String noString = "No", String cancelString = "Cancel")
        {
            Message = message;
            Yes = yesString;
            No = noString;
            Cancel = cancelString;
        }
    }
}
