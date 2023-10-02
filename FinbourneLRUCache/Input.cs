using FinbourneLRUCache;

namespace FinbourneLRUCache
{
    internal class Input
    {
        public void LaunchConsole() 
        { 
            LRUCache<int, string> cache = new LRUCache<int, string>(5);
            int key = 0;

            Console.WriteLine("Type 'exit' to terminate the console:\n");

            while (true)
            {
                try
                {
                    Console.WriteLine("Input any arbitrary data. Type 'Get' or 'Put' to place or retrieve items from the cache. To get the current cache size type 'Cache Size'. \n");
                    var input = Console.ReadLine();
                    int result;

                    if (input.Equals("exit"))
                    {
                        key = 0;
                        return;
                    }
                    else if (input == null)
                    {
                        throw new ArgumentNullException("You must enter a value. \n");
                    }
                    
                    if (input.Equals("Get", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Please type the key you wish to recieve: ");
                        input = Console.ReadLine();
                        if (int.TryParse(input, out result))
                        {
                            var retreivedCacheItem = cache.get(result);

                            if (retreivedCacheItem != null)
                            {
                                Console.WriteLine("The value associated with this key is: " + retreivedCacheItem + "\n");
                            }
                            else
                            {
                                Console.WriteLine("This key does not exist: " + retreivedCacheItem + "\n");
                            }
                        }
                    }
                    else if (input.Equals("Put", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Please type your desired key followed by the value you would like to associated with this key. \n");
                        input = Console.ReadLine();
                        if (int.TryParse(input, out result))
                        {
                            key = result;
                            input = Console.ReadLine();
                            cache.put(key, input);
                            Console.WriteLine("Key/Value pair succesfully added to cache.. \n");
                        }
                    }
                    else if (input.Equals("Cache Size", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("The cache size is: " + cache.CurrentCacheSize + "\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while tried to do something. Error: " + ex.Message + "\n");
                }
            }
        }
    }
}
