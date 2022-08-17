namespace CodeBase.Sounds.SoundPlay
{
    public class SoundPlayAtPoint : SoundPlayBase
    {
        protected override void PlaySound()
        {
            _audioService.PlaySound(SoundClip, transform.position, Volume);
        }
    }
}