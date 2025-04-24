namespace TipeEngine
{
    public static class ResourceManager
    {
        /*
        Generic Type-Based Loading
        `var obj = ResourceManager.Load<TestObject>();`
        Infers file path from type: assets/objects/TestObject.json
        Uses the type name as a convention-based key
        Calls a deserializer like JsonSerializer.Deserialize<TestObject>(json)
       */
    }
}
