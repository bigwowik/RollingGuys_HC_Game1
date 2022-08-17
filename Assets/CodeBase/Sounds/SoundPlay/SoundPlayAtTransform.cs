namespace CodeBase.Sounds.SoundPlay
{
    public class SoundPlayAtTransform : SoundPlayBase
    {
        protected override void PlaySound()
        {
            _audioService.PlaySound(SoundClip, transform, Volume);
        }
    }
}