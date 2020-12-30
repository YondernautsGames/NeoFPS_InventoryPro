using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using UnityEngine.Assertions;

namespace Devdog.General
{
    [RequireComponent(typeof (BoxCollider))]
    public partial class BoxTriggerRangeHandler : MonoBehaviour, ITriggerRangeHandler
    {
        [SerializeField]
        private Vector3 _volume = new Vector3(2f, 3f, 2f);
        public Vector3 volume
        {
            get { return _volume; }
        }

        private readonly List<Player> _playersInRange = new List<Player>();
        private BoxCollider _collider;
        private Rigidbody _rigidbody;
        private TriggerBase _trigger;

        protected void Awake()
        {
            _trigger = GetComponentInParent<TriggerBase>();
            Assert.IsNotNull(_trigger, "TriggerRangeHandler used but no TriggerBase found on object.");

            _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

            _collider = gameObject.GetOrAddComponent<BoxCollider>();
            _collider.isTrigger = true;
            _collider.center = new Vector3(0f, volume.y * 0.5f, 0f);
            _collider.size = volume;

            gameObject.layer = 2; // Ignore raycasts
        }

        public IEnumerable<Player> GetPlayersInRange()
        {
            return _playersInRange;
        }

        public bool IsPlayerInRange(Player player)
        {
            return _playersInRange.Contains(player);
        }

        protected void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _playersInRange.Add(player);
                player.triggerHandler.triggersInRange.Add(_trigger);
                _trigger.NotifyCameInRange(player);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _trigger.NotifyWentOutOfRange(player);
                player.triggerHandler.triggersInRange.Remove(_trigger);
                _playersInRange.Remove(player);
            }
        }
    }
}