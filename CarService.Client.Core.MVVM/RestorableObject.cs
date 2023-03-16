using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Reflection;

namespace CarService.Client.Core.MVVM
{
    public abstract class RestorableObject : ObservableObject
    {
        private readonly Dictionary<string, object?> propertySnapshots = new Dictionary<string, object?>();

        public bool HasChanged => HasChanges();

        public RestorableObject() : base()
        {
            CreateSnapshot();
        }

        public void RestoreSnapshot()
        {
            foreach (KeyValuePair<string, object?> keyValuePair in propertySnapshots)
            {
                PropertyInfo propInfo = this.GetType().GetProperty(keyValuePair.Key)!;

                if (propInfo.PropertyType.IsAssignableTo(typeof(IList)))
                {
                    IList? snapshotCollection = keyValuePair.Value as IList;
                    IList? currentCollection = propInfo.GetValue(this, null) as IList;

                    if (currentCollection == null) continue;

                    if (snapshotCollection == null)
                    {
                        currentCollection = null;
                        continue;
                    }

                    // Clear current collection
                    while (currentCollection.Count > 0)
                    {
                        currentCollection.RemoveAt(0);
                    }

                    // Copy items from snapshot to current collection
                    foreach (object item in snapshotCollection)
                    {
                        currentCollection.Add(item);
                    }

                }
                else
                {
                    propInfo.SetValue(this, keyValuePair.Value, null);
                }

                OnPropertyChanged(keyValuePair.Key);
            }

            propertySnapshots.Clear();
        }

        public void CreateSnapshot()
        {
            propertySnapshots.Clear();

            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanWrite))
            {
                if (propertyInfo.PropertyType.IsAssignableTo(typeof(IList)))
                {
                    IList collection = (IList)propertyInfo.GetValue(this, null)!;

                    propertySnapshots.Add(propertyInfo.Name, new ArrayList(collection));
                }
                else
                {
                    propertySnapshots.Add(propertyInfo.Name, propertyInfo.GetValue(this, null));
                }
            }
        }

        private bool HasChanges()
        {
            foreach (KeyValuePair<string, object?> keyValuePair in propertySnapshots)
            {
                object? snapshotValue = keyValuePair.Value;
                object? currentValue = this.GetType().GetProperty(keyValuePair.Key)!.GetValue(this, null);

                if (snapshotValue is IList)
                {
                    if (currentValue == null) return true;

                    IList snapshotCollection = (IList)snapshotValue;
                    IList currentCollection = (IList)currentValue;

                    if (snapshotCollection.Count != currentCollection.Count) return true;

                    foreach (object snapshotItem in snapshotCollection)
                    {
                        bool itemFoundInBothCollections = false;

                        foreach (object currentItem in currentCollection)
                        {
                            if (currentItem == snapshotItem)
                            {
                                itemFoundInBothCollections = true;
                            }
                        }

                        if (!itemFoundInBothCollections) return true;
                    }

                }
                else
                {
                    if (snapshotValue == null)
                    {
                        return currentValue != null;
                    }
                    else if (!snapshotValue.Equals(currentValue))
                    {
                        return true;
                    }
                }

            }

            return false;
        }
    }
}