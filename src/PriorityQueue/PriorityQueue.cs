using System;
using System.Collections.Generic;

namespace ReconArt.Collections
{
    /// <summary>
    /// Represents a Bucket Queue that supports only 6 priorities, but with constant time complexity for all of its operations.
    /// </summary>
    /// <remarks>
    /// This class is not thread-safe.
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public class PriorityQueue<TValue>
    {
        // 6 different priorities should be enough for normal use-cases. More than that - they should seek a different data structure altogether.
        private const int NUMBER_OF_BUCKETS = 6;
        private readonly LinkedList<TValue>[] _buckets = new LinkedList<TValue>[NUMBER_OF_BUCKETS];
        private LinkedListNode<TValue>? _head;
        private uint _minPriority = uint.MaxValue;
        private int _count;

        /// <summary>
        /// Initializes a new instance of <see cref="PriorityQueue{TValue}"/> class.
        /// </summary>
        public PriorityQueue()
        {
            for (int i = 0; i < NUMBER_OF_BUCKETS; i++)
            {
                _buckets[i] = new LinkedList<TValue>();
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="PriorityQueue{TValue}"/>.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Returns the object at the beginning of the <see cref="PriorityQueue{TValue}"/> without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">There are no elements in the queue.</exception>
        /// <returns>The object at the beginning of the <see cref="PriorityQueue{TValue}"/>.</returns>
        public TValue Peek()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("There are no elements in the queue.");
            }

            return _head.Value;
        }

        /// <summary>
        /// Adds an object with the specified <paramref name="priority"/> to the <see cref="PriorityQueue{TValue}"/>.
        /// </summary>
        /// <param name="value">The object to add.</param>
        /// <param name="priority">The object's priority. Must be a number in the range of 0 to 5. Lower is better.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="priority"/> is not in the accepted range.</exception>
        /// <returns><see cref="LinkedListNode{TValue}"/> of the added object.</returns>
        public LinkedListNode<TValue> Enqueue(TValue value, in uint priority)
        {
            if (priority > NUMBER_OF_BUCKETS - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(priority), priority, $"Value must be in the range of 0 to {NUMBER_OF_BUCKETS - 1}.");
            }

            LinkedList<TValue> bucket = _buckets[priority];
            LinkedListNode<TValue> node = bucket.AddLast(value);
            _count++;

            if (_minPriority > priority)
            {
                _minPriority = priority;
                _head = node;
            }

            return node;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="PriorityQueue{TValue}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">There are no elements in the queue.</exception>
        /// <returns>The object at the beginning of the <see cref="PriorityQueue{TValue}"/>.</returns>
        public TValue Dequeue()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("There are no elemments in the queue.");
            }

            LinkedList<TValue> bucket = _buckets[_minPriority]!;
            bucket.Remove(_head);
            _count--;

            TValue returnValue = _head.Value;

            RecalculateHeadAndMinPriority(bucket);

            return returnValue;
        }


        /// <summary>
        /// Attempts to remove an object in the <see cref="PriorityQueue{TValue}"/> by its <see cref="LinkedListNode{TValue}"/> and priority.
        /// </summary>
        /// <param name="node">The object's <see cref="LinkedListNode{TValue}"/>.</param>
        /// <param name="priority">The object's priority. Must be a number in the range of 0 to 5. Lower is better.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="priority"/> is not in the accepted range.</exception>
        /// <returns><see langword="true"/> if the object was successfully removed, <see langword="false"/> otherwise.</returns>
        public bool TryRemove(in LinkedListNode<TValue> node, in uint priority)
        {
            if (priority < 0 || priority > NUMBER_OF_BUCKETS - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(priority), priority, $"Value must be in the range of 0 to {NUMBER_OF_BUCKETS - 1}.");
            }

            if (node.List is null)
            {
                return false;
            }

            LinkedList<TValue> bucket = _buckets[priority];
            if (bucket != node.List)
            {
                return false;
            }

            bucket.Remove(node);
            _count--;

            if (node == _head)
            {
                RecalculateHeadAndMinPriority(bucket);
            }

            return true;
        }

        #region Private_methods

        private void RecalculateHeadAndMinPriority(in LinkedList<TValue> bucket)
        {
            _head = null;

            if (bucket.Count == 0)
            {
                while (++_minPriority < _buckets.Length)
                {
                    if (_buckets[_minPriority].Count != 0)
                    {
                        _head = _buckets[_minPriority].First;
                        break;
                    }
                }
            }
            else
            {
                _head = bucket.First;
            }
        }

        #endregion
    }
}
