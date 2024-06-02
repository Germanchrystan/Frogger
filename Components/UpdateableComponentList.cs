using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prototypes;

namespace Components
{
  public class UpdateableIterableComponentList<UpdateableIterableComponent, UpdateArgument>
  {
    private int activeNum = 0;
    public List<UpdateableIterableComponent<UpdateArgument>> components = new List<UpdateableIterableComponent<UpdateArgument>>();
    public UpdateableIterableComponentList(){}

    public void AddComponent(UpdateableIterableComponent<UpdateArgument> component)
    {
      if (component.Active) AddActive(component);
      else AddInactive(component);
    }
    private void AddActive(UpdateableIterableComponent<UpdateArgument> newComponent)
    {
      components.Add(newComponent);
      newComponent.OnSetActive += OnComponentSetActive;
      ActivateComponent(newComponent);
    }

    private void AddInactive(UpdateableIterableComponent<UpdateArgument> newComponent)
    {
      components.Add(newComponent);
      newComponent.OnSetActive += OnComponentSetActive;
    }

    public void ActivateComponent(UpdateableIterableComponent<UpdateArgument> component)
    {
      UpdateableIterableComponent<UpdateArgument> firstInac = components[activeNum];
      if (firstInac != component)
      {
        int newPos = components.Count - 1;
        UpdateableIterableComponent<UpdateArgument> temp = firstInac;
        components[activeNum] = component;
        components[newPos] = temp;
      }
      activeNum++;
    }

    public void DeactivateComponent(UpdateableIterableComponent<UpdateArgument> component)
    {
      UpdateableIterableComponent<UpdateArgument> lastAct = components[activeNum - 1];
      int compPos = components.IndexOf(component);
      if (lastAct != component)
      {
        UpdateableIterableComponent<UpdateArgument> temp = lastAct;
        components[activeNum - 1] = component;
        components[compPos] = temp;
      }
      activeNum--;
    }

    public void OnComponentSetActive(object source, bool isActive)
    {
      if (!isActive) DeactivateComponent((UpdateableIterableComponent<UpdateArgument>)source);
      else ActivateComponent((UpdateableIterableComponent<UpdateArgument>)source);
    }

    public void Clear()
    {
      components.Clear();
    }

    public void Update(UpdateArgument updateArgument)
    {
      for (int i = 0; i < activeNum; i++)
      {
        components[i]?.Update(updateArgument);
      }
    }
  }
}