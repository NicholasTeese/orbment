using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public struct StatusEffectItem
    {
        public StatusEffect.Status m_type;
        public StatusEffect m_script;
        public GameObject m_object;
        
    }

   
    public int m_poolAmount = 50;
    //originals
    public GameObject[] m_statusEffects;

    private List<List<StatusEffectItem>> m_pool = new List<List<StatusEffectItem>>();
    // Use this for initialization
    void Start()
    {
        for (int numType = 0; numType < m_statusEffects.Length; ++numType)
        {
            List<StatusEffectItem> itemList = new List<StatusEffectItem>();
            for(int i = 0; i < m_poolAmount; ++i)
            {
                StatusEffectItem newStatusEffect = new StatusEffectItem();

                newStatusEffect.m_object = Instantiate(m_statusEffects[numType], this.transform);



                StatusEffect effectScript = newStatusEffect.m_object.GetComponent<StatusEffect>();

                if (effectScript != null)
                {
                    effectScript.m_manager = this.transform;
                    newStatusEffect.m_type = effectScript.m_type;
                    newStatusEffect.m_script = effectScript;

                }


                
                newStatusEffect.m_object.SetActive(false);
                newStatusEffect.m_object.transform.parent = this.transform;


                itemList.Add(newStatusEffect);

            }

            m_pool.Add(itemList);

        }

    }

    public void RequestEffect(Transform obj, StatusEffect.Status a_type)
    {
        GameObject effect = null;

        for(int i = 0; i< m_pool.Count; ++i)
        {
            if(m_pool[i][0].m_type == a_type)
            {
                effect = FindInactive(m_pool[i]);
                break;
            }
        }

        if(effect != null)
        {
            effect.transform.position = obj.position;
            effect.transform.SetParent(obj);
            effect.SetActive(true);
        }
    }

    GameObject FindInactive( List<StatusEffectItem> a_list)
    {
        for (int i = 0; i < a_list.Count; ++i)
        {
            if (!a_list[i].m_object.activeInHierarchy)
            {
                return a_list[i].m_object;

            }
        }

        return null;

    }

}
