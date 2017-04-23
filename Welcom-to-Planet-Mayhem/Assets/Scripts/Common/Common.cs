//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

namespace WelcomeToPlanetMayhem
{
	public static class Common
	{
		public static string MoveInput = "Vertical_";
		public static string TurnInput = "Horizontal_";
		public static string YawInput = "CameraYaw_";
		public static string ZoomInput = "CameraPitch_";
		public static string JumpInput = "Jump_";
		public static string ShootInput = "Shoot_";
		public static string SwitchLeftInput = "SwitchLeft_";
		public static string SwitchRightInput = "SwitchRight_";
		public static string StartInput = "Start_";
		public static string ReloadInput = "Reload_";
		public static string UseToolInput = "UseTool_";
		public static string ConfirmInput = "Confirm_";
		public static string CancelInput = "Cancel_";

		public enum GameState
		{
			Loading,
			Menu,
			Playing,
			Paused,
			GameOver
		}

		public static GameState State;
	}
}