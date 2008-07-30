package steamcondenser;

public class Helper
{
	public static byte[] byteArrayFromInteger(int integer)
	{
		return new byte[] {
				(byte) (integer >> 24),
				(byte) (integer >> 16),
				(byte) (integer >> 8),
				(byte) integer
		};
	}
	
	public static int integerFromByteArray(byte[] byteArray)
	{
		return	byteArray[0] << 24 |
				(byteArray[1] & 0xff) << 16 |
				(byteArray[2] & 0xff) << 8 |
				(byteArray[3] & 0xff);
	}
}
