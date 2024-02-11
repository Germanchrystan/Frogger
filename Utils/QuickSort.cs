using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Components;

namespace Utils
{
  class QuickSort
  {
    public static void Sort(List<Renderer> list, int left, int right)
    {
      if (left < right)
      {
        int pivot = Partition(list, left, right);
        Sort(list, left, pivot - 1);
        Sort(list, pivot + 1, right);
      }
    }
    private static int Partition(List<Renderer> list, int left, int right)
    {
      int pivot = list[right].Layer;
      int i = left - 1;

      for(int j = left; j < right; j++)
      {
        if (list[j].Layer <= pivot)
        {
          i++;
          Renderer temp = list[i];
          list[i] = list[j];
          list[j] = temp;
        }
      }
      
      Renderer temp1 = list[i+1];
      list[i+1] = list[right];
      list[right] = temp1;

      return i + 1;
    }
  }
}