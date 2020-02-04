using System;

namespace Framework.Repositories
{
    public class EntityDeletingEventArgs<T> : EventArgs
    {
        public T SavedEntity;

    }
}

