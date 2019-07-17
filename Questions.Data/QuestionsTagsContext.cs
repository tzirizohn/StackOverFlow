using System;

namespace StackOverFlow.Data
{
    internal class QuestionsTagsContext : IDisposable
    {
        private string _connectionString;

        public QuestionsTagsContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}