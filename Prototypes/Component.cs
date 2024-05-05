
using System;

namespace Prototypes
{
    public interface Component
    {
        public string Type();
    }

    public interface Updateable<T>
    {
       public void Update(T t);
    }
    public class Iterable
    {
        private bool active;

        public bool Active { get { return active; } set { active = value; SetActive(); } }

        public event EventHandler<bool> OnSetActive;   
        private void SetActive()
        {  
            if (OnSetActive != null) OnSetActive(this, active);
        }
    }
    public abstract class UpdateableIterableComponent<UpdateArgument>: Iterable, Updateable<UpdateArgument>, Component
    {  
        public abstract string Type();
        public abstract void Update(UpdateArgument t);
        private bool active;
        new public bool Active { get { return active; } set { active = value; SetActive(); } }
        new public event EventHandler<bool> OnSetActive;   
        private void SetActive()
        {  
            if (OnSetActive != null) OnSetActive(this, active);
        }
    }
}
