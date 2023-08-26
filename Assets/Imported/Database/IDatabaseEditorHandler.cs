namespace Developed.Database
{
    internal interface IDatabaseEditorHandler
    {
        void UpdateDatabase();
        string GetContents();
    }
}