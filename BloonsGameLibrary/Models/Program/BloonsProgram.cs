namespace BloonsProject
{
    public static class BloonsProgram
    {
        public static void Main()
        {
            var map = new MediumMap();
            var programController = new ProgramController(map);
            programController.RunGame();
        }
    }
}