namespace CodeExercise.DressCode.Console
{
    public class Writer : IWriter
    {
        public void Write(string value)
        {
            System.Console.Write(value);
        }
        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
