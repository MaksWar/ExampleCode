using Additions.Missions;
using Duckject.Core.Attributes;
using Game.Cooking.Core;

namespace Game.Cooking.Missions
{
	public class InitializeMission : MissionBase
	{
		private IInputController _inputController;
		private WishCloud _wishCloud;

		[Quack]
		private void Construct(IInputController inputController, WishCloud wishCloud)
		{
			_inputController = inputController;
			_wishCloud = wishCloud;
		}

		protected override void Mission()
		{
			_inputController.LockAllInput();
			_wishCloud.Hide(0);

			End();
		}
	}
}