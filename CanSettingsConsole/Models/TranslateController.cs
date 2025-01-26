namespace CanSettingsConsole.Models
{
    public class TranslateController : ControllerBase
    {
        public override string Name => "Контроллер 2 уровня";
        public bool IsDuplicate { get; set; }

        public override string ToString()
        {
            return $"{Status}|{ParentId}|{Id}|{Code}|{(IsDuplicate ? 1 : 0)}";
        }
        public override void Initialize(string[] array)
        {
            base.Initialize(array);
            if (array.Length < 7) return;
            IsDuplicate = array[6] == "1";
        }
    }
}
