namespace Infrastructure.Services.Input
{
    public class AndroidInput : IInput
    {

        public float RotateInput { get; set; }
        public float GasInput { get; set; }
        

        public void UpdateSpeed( float speed)
        {
            GasInput = speed;
        }

        public void UpdateRotation(float rotation)
        {
            RotateInput = rotation;
        }
    }
}