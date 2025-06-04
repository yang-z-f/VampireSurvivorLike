using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool<T>
{
      void Get();
      void Release(T gameobject);
      void Create(T gameobject);
}
