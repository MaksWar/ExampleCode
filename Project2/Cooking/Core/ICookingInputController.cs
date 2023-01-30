using Game.Cooking.Meals.Data;

namespace Game.Cooking.Core
{
	public interface ICookingInputController : IInputController
	{
		void LockPartHolder(TypeOfPart typeOfPart);
	}
}