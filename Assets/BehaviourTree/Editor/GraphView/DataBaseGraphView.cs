using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Data;
using UnityEditor.Experimental.GraphView;

namespace BehaviourTree.Edit
{
    public class DataBaseGraphView : GraphView
    {
        List<TreeDataBase> _dataList;

        public DataBaseGraphView(List<TreeDataBase> list = null) : base()
        {
            style.flexGrow = 1;
            style.flexShrink = 1;

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            Insert(0, new GridBackground());

            _dataList = list;
        }
    }
}