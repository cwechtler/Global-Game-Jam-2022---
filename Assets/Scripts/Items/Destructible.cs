using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum destructibleStates 
{ 
	Fixed,
	Cracked,
	Broken
}

public class Destructible : MonoBehaviour
{
	[SerializeField] private skillElementType skillRequiredToDestroy;
	[Space]
	[Tooltip("Check box to select a prefab item to be dropped on destroy")]
	[SerializeField] private bool dropItem = true;
	[ConditionalHide("dropItem", true)]
	[SerializeField] private GameObject prefabToDrop;
	[SerializeField] private AudioClip[] sfxClips;
	[Space]
	[Tooltip("Fixed to broken order")]
	[SerializeField] private Sprite[] destructibleSpriteStates;

	private bool dropped;
	private destructibleStates State;
	private SpriteRenderer spriteRenderer;
	private CapsuleCollider2D capsuleCollider2D;
    void Start()
    {
		State = destructibleStates.Fixed;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
		print("Start: " + State);
	}

    private void UpdateSprite()
    {
		switch (State)
		{
			case destructibleStates.Fixed:
				spriteRenderer.sprite = destructibleSpriteStates[0];
				State = destructibleStates.Cracked;
				print("Fixed: " + State);
				break;
			case destructibleStates.Cracked:
				spriteRenderer.sprite = destructibleSpriteStates[1];
				State = destructibleStates.Broken;
				capsuleCollider2D.enabled = false;
				print("Cracked: " + State);
				if (dropItem)
				{
					GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
				}
				break;
			case destructibleStates.Broken:				
				break;
            default:
				break;
        }
			
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Skill")) {
			LightningBoltMaster skill = collision.GetComponentInParent<LightningBoltMaster>();
			if (skill.SkillElementType == skillRequiredToDestroy) {
				//dropped = true;
				float skillDuration = skill.Duration;
				int randomClip = Random.Range(0, sfxClips.Length - 1);
				//GameObject.Destroy(this.gameObject);
				UpdateSprite();
				SoundManager.instance.PlayDestructibleSound(sfxClips[randomClip]);
			}
		}
	}

	private void OnParticleCollision(GameObject particle)
	{
		SkillConfig particleParent = particle.GetComponentInParent<SkillConfig>();
		if (particleParent.SkillElementType == skillRequiredToDestroy && !dropped) {
			dropped = true;
			int randomClip = Random.Range(0, sfxClips.Length - 1);
			//if (dropItem) {
			//	GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
			//}
			//GameObject.Destroy(this.gameObject);
			State = destructibleStates.Cracked;
			UpdateSprite();
			SoundManager.instance.PlayDestructibleSound(sfxClips[randomClip]);
		}
	}
}
