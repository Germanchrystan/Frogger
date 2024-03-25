using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Components;

namespace Utils
{
  class QuickSort
  {
    public static void Sort(List<Renderable> list, int left, int right)
    {
      if (left < right)
      {
        int pivot = Partition(list, left, right);
        Sort(list, left, pivot - 1);
        Sort(list, pivot + 1, right);
      }
    }
    private static int Partition(List<Renderable> list, int left, int right)
    {
      int pivot = list[right].Layer;
      int i = left - 1;

      for(int j = left; j < right; j++)
      {
        if (list[j].Layer <= pivot)
        {
          i++;
          Renderable temp = list[i];
          list[i] = list[j];
          list[j] = temp;
        }
      }
      
      Renderable temp1 = list[i+1];
      list[i+1] = list[right];
      list[right] = temp1;

      return i + 1;
    }
  }
}