using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDatastructures.Core;

namespace CustomCollections
{
    /// <summary>
    /// Generic class that implements the IEnumerable<T> interface to make the ObservableList enumerable i.e. possibility to iterate
    /// through the content of the list. The IObservaleList<T> interface defines all the operations that this class needs to provide.
    /// </summary>
    public class ObservableList<T> : IEnumerable<T>, IObservableList<T>
    {
        // A private variabel to work as the internal data storage for the list
        private readonly List<T> internalStorage = new List<T>();


        // Two generic delegates EventHandlers that represents the method that will handle an event when the event provides data.
        public event EventHandler<CustomDatastructures.Core.RejectableEventArgs<T>> BeforeChange;
        public event EventHandler<ListChangedEventArgs<T>> Changed;


        //Methods that checks if there are subscribers to an event, if there is, the event is raised
        protected virtual void OnChanged (object sender, ListChangedEventArgs<T> args)
        {
            Changed?.Invoke(this, args);
        }

        protected virtual void OnBeforeChange(RejectableEventArgs_Subclass<T> args)
        {
            BeforeChange?.Invoke(this, args);
        }


        //Methods to manipulate the internal storage
        public void Add(T item) 
        {
           
            //Before a change is about to happen we have to check if any subscriber wants to reject the change
            var beforeAdd = new RejectableEventArgs_Subclass<T>(Operation.Add, item, internalStorage.Count);   
            OnBeforeChange(beforeAdd);

            //Check if any subscriber rejected the change, if not, go on and make the change
            if (beforeAdd.IsOperationRejected == false) {
                internalStorage.Add(item);
                OnChanged(this, new ListChangedEventArgs<T>(Operation.Add, item, internalStorage.Count));
            }   
            else
            {
                throw new OperationRejectedException("You can not add this item!"); 
            }
        }

        public void Remove(T item) 
        {

            //Before a change is about to happen we have to check if any subscriber wants to reject the change
            var beforeRemove = new RejectableEventArgs_Subclass<T>(Operation.Remove, item, internalStorage.Count);
            OnBeforeChange(beforeRemove);

            // If the wanted item to remove is not on the list then throw InvalidOperationExeption
            if (Contains(item))
            {
                //If change is rejected then throw OperationRejectedExeption
                if (beforeRemove.IsOperationRejected == false)
                {               
                    internalStorage.Remove(item);
                    OnChanged(this, new ListChangedEventArgs<T>(Operation.Remove, item, internalStorage.Count));
                }
                else
                {
                    throw new OperationRejectedException("You can not remove this item!");
                }
            }
            else
            {
                throw new InvalidOperationException("This item is not on the list!");              
            }          
        }

        /// <summary>
        /// Method that checks if the internal storage list contains an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>bool</returns>
        public bool Contains(T item) {

            return internalStorage.Contains(item);
        }

        /// <summary>
        /// Instead of using the real add method, this methods allows you to explore what you are able to add and not, without handling exceptions.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>bool</returns>
        public bool TryAdd(T item)
        {
            var tryAdd = new RejectableEventArgs_Subclass<T>(Operation.Add, item, internalStorage.Count);
            OnBeforeChange(tryAdd);

            if (tryAdd.IsOperationRejected == false)
            {
                internalStorage.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Instead of using the real remove method, this methods allows you to explore what you are able to remove and not, without handling exeptions. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>bool</returns>
        public bool TryRemove(T item)
        {
            var tryRemove = new RejectableEventArgs_Subclass<T>(Operation.Remove, item, internalStorage.Count);
            OnBeforeChange(tryRemove);
   
            if (Contains(item))
            {
                if (tryRemove.IsOperationRejected == false)
                {
                    internalStorage.Remove(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// The GetEnumerator method is required by the IEnumerable and IEnumerable<T> interfaces. It is implemented so that instances from the ObservableList class can be used in a foreach-loop.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return internalStorage.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)internalStorage).GetEnumerator();
        }


    }
}
