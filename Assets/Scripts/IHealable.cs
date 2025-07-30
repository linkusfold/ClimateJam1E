/*
 * -----------------------------------------------
 * IHealable.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Interface for objects that can be healed.
 * Any GameObject that wants to receive healing must implement this.
 * -----------------------------------------------
 */

public interface IHealable
{
    // Applies healing to the object
    void Heal(int amount);
}