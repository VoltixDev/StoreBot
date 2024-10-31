using System.Reflection;
using JetBrains.Annotations;
using Serilog;

namespace DiscordBot.Events;

[AttributeUsage(AttributeTargets.Method)]
[PublicAPI]
public class EventAttribute(string name) : Attribute
{
    public string Name => name;

    public static void RegisterEvents<TSource, TDest>(TSource source, TDest dest)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(dest);

        var sourceType = typeof(TSource);
        var sourceMethods = sourceType.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(x => x.GetCustomAttribute<EventAttribute>() != null);

        var destType = typeof(TDest);
        var destEvents = destType.GetEvents();
        
        var mapping = sourceMethods.ToDictionary(x => x, x => destEvents.FirstOrDefault(y => y.Name == x.GetCustomAttribute<EventAttribute>()?.Name));

        foreach (var (method, ev) in mapping)
        {
            Log.Debug("Trying to bind {Method} to {Event}...", method.Name, ev?.Name);
            var attr = method.GetCustomAttribute<EventAttribute>();
            
            if (ev is null)
            {
                Log.Warning("Method {Method} with specified event name {EventName} has no mapping in {DestType}", $"{method.Name}@{sourceType.Name}", attr?.Name, destType.Name);
                continue;
            }

            try
            {
                ev.AddEventHandler(dest, Delegate.CreateDelegate(ev.EventHandlerType!, source, method));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred binding {Name}.", attr?.Name);
                Log.Debug("Method declaring type matches source type: {Check}", method.DeclaringType == sourceType);
                Log.Debug("Event handler type: {EHT}", ev.EventHandlerType);
            }
        }
    }
}