namespace Infrastructure.Cachings
{
    public interface ICacheStore
    {
        //void Add<TItem>(ICacheKey<TItem> key, TItem item);
        //TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class;
        //void Remove<TItem>(ICacheKey<TItem> key);

        void Add<TItem>(string key, TItem item);
        TItem Get<TItem>(string key) where TItem : class;
        void Remove<TItem>(string key);

    }
}
