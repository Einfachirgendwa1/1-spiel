namespace Guns.Behaviours {
    public class Shoot : Base {
        internal override State State => State.Shoot;

        protected override void Action() {
            base.Action();
            Gun.Shoot(Repetitions);
        }
    }
}