using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace data
{
    //��ƼƼ Ŭ����
    [Serializable]
    public class Stat
    {
        //����(��Ű ����)
        public int level;
        public int hp;
        //���ݷ�
        public int offence;
        //����
        public int defence;
        //�̵��ӵ�
        public float move_speed;
        //���ݻ�Ÿ�
        public float attack_distance;
        //���ݼӵ�
        public float attack_speed;
        //����ü �ӵ�
        public float projectile_speed;
    }

    [Serializable]
    public class StatLoader : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
            {
                dict.Add(stat.level, stat);
            }

            return dict;
        }
    }
}
