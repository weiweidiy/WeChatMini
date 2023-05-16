using HiplayGame;
using System;
using UnityEngine;
//using EventArgs = System.EventArgs;

namespace EnhancedScrollerAdvance
{

    /// <summary>
    /// 所有Cell的业务逻辑继承自这个类
    /// </summary>
    public abstract class EnhancedUnitViewV2 : IEnhancedUnitViewV2
    {
        /// <summary>
        /// 选中委托，子类中如果需要选中，则需要调用
        /// </summary>
        public event EventHandler onSelected;
        public event Action<string, object> onUnitCustomEvent;

        protected GameObject _go;
        protected EnhancedDataV2 _data;
        BindingComponents _bindings;

        //public int DataIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cellGo"> EnhancedCellViewAdvance组件中，RowCells数组中拖拽的element对象,即单个cell游戏对象 </param>
        public void Initialize(GameObject cellGo)
        {
            _go = cellGo;
            _bindings = _go.GetComponent<BindingComponents>();
            OnInitialize();
        }

        /// <summary>
        /// 刷新视图
        /// </summary>
        /// <param name="cellData">对应的数据</param>
        public void RefreshCellView(EnhancedDataV2 cellData)
        {
            _go.SetActive(cellData != null);

            if (cellData == null)
                return;

            _data = cellData;

            OnRefreshCellView();
        }

        /// <summary>
        /// 刷新选中状态，可在子类重写
        /// </summary>
        /// <param name="selected"></param>
        public virtual void RefreshSelectedStatus(bool selected) { }

        /// <summary>
        /// 子类实现，初始化逻辑: 查找缓存ui组件
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// 子类实现，刷新视图：赋值
        /// </summary>
        public abstract void OnRefreshCellView();


        protected void SelectionClicked(object sender)
        {
            onSelected?.Invoke(this, new System.EventArgs());
        }

        /// <summary>
        /// 发送自定义事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        protected void SendUnitCustomEvent(string eventName, object args)
        {
            onUnitCustomEvent?.Invoke(eventName, args);
        }

        public virtual void OnVisibilityChanged() { }

        protected UnityEngine.Object GetBindingComponent(string name)
        {
            return _bindings.GetBindingComponent(name);
        }
    }
}