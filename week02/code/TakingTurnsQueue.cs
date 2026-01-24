

/// <summary>
/// Implements a circular queue of people where each person is given a number of turns.
/// People are added to the back of the queue (FIFO). Each time a person takes a turn,
/// they may be added back to the queue depending on how many turns they have remaining.
///
/// Rules:
/// - If a person's turns are greater than 0, one turn is consumed each time they are returned.
/// - If a person's turns reach 0, they are removed from the queue permanently.
/// - If a person's turns are 0 or less initially, they have infinite turns and are
///   always added back to the queue without modifying the turns value.
/// - If the queue is empty when attempting to get the next person, an exception is thrown.
/// </summary>
public class TakingTurnsQueue
{
    // Internal queue used to store Person objects in FIFO order
    private Queue<Person> _queue = new Queue<Person>();

    // Returns the current number of people in the queue
    public int Length => _queue.Count;

    /// <summary>
    /// Adds a new person to the back of the queue with the specified number of turns.
    /// </summary>
    /// <param name="name">The name of the person</param>
    /// <param name="turns">
    /// The number of turns the person has.
    /// A value of 0 or less represents an infinite number of turns.
    /// </param>
    public void AddPerson(string name, int turns)
    {
        _queue.Enqueue(new Person(name, turns));
    }

    /// <summary>
    /// Removes the next person from the front of the queue and returns them.
    /// The person is added back to the queue if they still have turns remaining
    /// or if they have infinite turns (turns <= 0).
    ///
    /// Throws an InvalidOperationException if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        // If the queue is empty, throw the required exception
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        // Remove the next person from the front of the queue
        var person = _queue.Dequeue();

        // Case 1: Infinite turns (0 or less)
        // The person is always re-added without modifying the turns value
        if (person.Turns <= 0)
        {
            _queue.Enqueue(person);
        }
        // Case 2: Finite turns
        else
        {
            // Consume one turn
            person.Turns--;

            // Re-add the person only if they still have turns remaining
            if (person.Turns > 0)
            {
                _queue.Enqueue(person);
            }
        }

        // Return the person who took the turn
        return person;
    }
}
