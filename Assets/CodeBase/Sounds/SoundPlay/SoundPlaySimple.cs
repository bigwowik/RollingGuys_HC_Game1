namespace CodeBase.Sounds.SoundPlay
{
    public class SoundPlaySimple : SoundPlayBase
    {
        protected override void PlaySound()
        {
            _audioService.PlaySound(SoundClip, Volume);
        }
    }
}