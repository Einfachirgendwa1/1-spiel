namespace Guns.Behaviours {
    public class Shoot : Base {
        internal override State state => State.Shoot;

        protected override void Action() {
            base.Action();
            gun.Shoot(repetitions);
        }
    }
}