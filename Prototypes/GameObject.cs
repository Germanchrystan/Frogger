using System.Collections.Generic;

namespace Prototypes
{
 public class GameObject
 {
    private Dictionary<string, Component> components = new Dictionary<string, Component>();
    public Dictionary<string, Component> Components { get { return components; } }

    public GameObject AddComponent(string name, Component component)
    {
      components.Add(name, component);
      return this;
    }

    public T GetComponent<T>(string name)
    {
      return (T)components[name];
    }
 } 
}