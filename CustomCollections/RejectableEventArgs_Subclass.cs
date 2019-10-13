using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomDatastructures.Core;

namespace CustomCollections
{
    /// <summary>
    /// Class that handle rejects from subscribers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RejectableEventArgs_Subclass<T> : CustomDatastructures.Core.RejectableEventArgs<T> 
    {
        // The property to use to get the answer to if the operation is rejected or not. False (not rejected) at start.
        private bool isRejected = false;

        //Construct with no functionality except defining the parameters an instance of this class must have.
        public RejectableEventArgs_Subclass(Operation operation, T value, int count) : base (operation, value, count)
        {
        }

        /// <summary>
        /// Method that returs the property that is true if the operation is rejected and false if it is not rejected.
        /// </summary>
        public override bool IsOperationRejected { get { return isRejected; } }

        /// <summary>
        /// If this method is called it changes the operation is rejected and therefore the property is changed.
        /// </summary>
        public override void RejectOperation() { isRejected = true; }


   }
}