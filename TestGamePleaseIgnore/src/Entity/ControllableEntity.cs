using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.game;

namespace TestGamePleaseIgnore.src.Entity
{
    public class ControllableEntity : GravityEntity
    {
        public enum EntityState
        {
            Idle, Walking, Jumping, Falling
        }

        protected float AccelerationX { get; set; }
        protected float AccelerationY { get; set; }

        protected float MaxSpeedX { get; set; }
        protected float MaxSpeedY { get; set; }
        protected float JumpHeight { get; set; }

        protected EntityState State;

        private bool isIdle;
        private bool isJumping;
        private bool hasJumped;

        public ControllableEntity(float x, float y, float width, float height) : base(x, y, width, height)
        {
            AccelerationX = 0.05f;
            AccelerationY = 1f;
            MaxSpeedX = 3f;
            MaxSpeedY = 3f;
            State = EntityState.Idle;
            isIdle = true;
            isJumping = false;
            hasJumped = false;
        }

        protected void ChangeState(EntityState newState)
        {
            //Make sure the character always makes the jump animation while jumping.
            if (State != EntityState.Jumping)
            {
                if (State != newState)
                {
                    State = newState;
                    UpdateStates();
                }
            }
        }

        protected void UpdateStates()
        {
            if (State == EntityState.Walking)
                SetTexture(GameTextures.PenguinWalk);
            else if (State == EntityState.Jumping)
                SetTexture(GameTextures.PenguinJump);
            else
                SetTexture(GameTextures.PenguinIdle);
        }

        public virtual void MoveLeft()
        {
            SpeedX -= AccelerationX;
            if (SpeedX < -MaxSpeedX)
                SpeedX = -MaxSpeedX;
            ChangeState(EntityState.Walking);
            Mirrored = true;
            isIdle = false;
        }

        public virtual void MoveRight()
        {
            SpeedX += AccelerationX;
            if (SpeedX > MaxSpeedX)
                SpeedX = MaxSpeedX;
            ChangeState(EntityState.Walking);
            Mirrored = false;
            isIdle = false;
        }

        public virtual void Jump()
        {
            if(!hasJumped)
            {
                ChangeState(EntityState.Jumping);
                isJumping = true;
                hasJumped = true;
                isIdle = false;
            }
        }

        public override void Update(long elapsedTime)
        {
            if (isJumping)
            {
                SpeedY -= AccelerationY;
                if (SpeedY < -MaxSpeedY)
                {
                    SpeedY = -MaxSpeedY;
                    isJumping = false;
                }     
            }
            if (SpeedY > 0)
            {
                State = EntityState.Idle;
                ChangeState(EntityState.Falling);
            }
            if (!IsAirborne)
                hasJumped = false;
            if (isIdle)
                ChangeState(EntityState.Idle);
            base.Update(elapsedTime);

            isIdle = true;
        }
    }
}
