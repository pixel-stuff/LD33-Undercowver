using UnityEngine;
using System.Collections;

public class UpdateLightReception : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        /*GameObject player = GameObject.FindGameObjectWithTag("PlayerManager");
        Vector4 playerPos = (new Vector4(player.transform.position.x, player.transform.position.y, player.transform.position.z, 0.0f));
        Vector4 playerRot = (new Vector4(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, 0.0f));
        Vector4 playerLRay = (player.transform.rotation*(new Vector4(0.0f, -1.0f, 0.0f, 0.0f)));
        Vector4 oNRay = (new Vector4(transform.position.x, transform.position.y, transform.position.z, 0.0f))+(new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
        Vector4 LRay = new Vector4(transform.position.x - player.transform.position.x,
            transform.position.y - player.transform.position.y,
            0.0f,
            0.0f);
        // (new Vector4(transform.position.x, transform.position.y + 1.0f, transform.position.z, 0.0f));
        this.gameObject.GetComponent<SpriteRenderer>().material.SetVector("_NormalRay", playerPos+playerLRay);
        this.gameObject.GetComponent<SpriteRenderer>().material.SetVector("_LightRay", LRay);*/
        GameObject player = GameObject.FindGameObjectWithTag("PlayerManager");
        Vector4 pPos = new Vector4(player.transform.position.x,
             player.transform.position.y,
             0.0f,
             0.0f);
        Vector4 oPos = new Vector4(transform.position.x,
             transform.position.y,
             0.0f,
             0.0f);
        Texture tex2D = this.gameObject.GetComponent<SpriteRenderer>().material.GetTexture("_MainTex");
        if (tex2D != null) {
            float ratio = tex2D.width / tex2D.height;
            this.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_XUVRatio", ratio);
            ratio = tex2D.height / tex2D.width;
            this.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_YUVRatio", ratio);
        }
        this.gameObject.GetComponent<SpriteRenderer>().material.SetVector("_NormalRay", oPos);
        this.gameObject.GetComponent<SpriteRenderer>().material.SetVector("_LightRay", pPos);
    }
}
