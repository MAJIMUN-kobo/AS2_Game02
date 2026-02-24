using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableLoader<T>
{
    public Task<Result> data { get; private set; }
    public string assetPath { get; private set; }
    public bool isLoaded { get; private set; }
    public bool onLoaded { get; private set; }

    public class Result
    {
        public IList<T> value { get; }
        public bool isSuccess { get; }
        public string error { get; }

        public Result(IList<T> value, bool isSuccess, string error)
        {
            this.value = value;
            this.isSuccess = isSuccess;
            this.error = error;
        }

        public static Result Success(IList<T> value)
        {
            return new Result(value, true, null);
        }

        public static Result Failure(string error)
        {
            return new Result(default, false, error);
        }
    }

    public AddressableLoader(string assetPath)
    {
        this.assetPath = assetPath;
        this.isLoaded = false;
        this.onLoaded = false;

        data = Load();
    }

    public void Update()
    {
        if (!isLoaded)
        {
            Debug.Log($"{ data }");
        }
    }

    private async Task<Result> Load()
    {
        var path = assetPath;
        var assetHandle = Addressables.LoadAssetsAsync<T>(path);
        var asset = await assetHandle.Task;
        var loaded = false;

        var result = Result.Success(asset);
        isLoaded = true;
        if (isLoaded && isLoaded != loaded)
            onLoaded = true;

        onLoaded = loaded;
        return result;
    }
}
