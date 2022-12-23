export default {
  system: {
    hookComponent: {
      dialog: {
        defaultClose: '关闭',
        defaultConfirm: '确认',
        defaultTitle: '提示'
      }
    },
    page: {
      add: '新增',
      update: '修改',
      refresh: '刷新',
      export: '导出',
      operate: '操作',
      edit: '编辑',
      delete: '删除',
      close: '关闭',
      submit: '提交'
    },
    tips: {
      success: '成功!',
      fail: '失败!',
      beforeDeleteMessage: '您确定要删除此条数据吗?',
      beforeDeleteDetailMessage: '您确定要删除此行吗?',
      detailLengthIsZero: '请添加明细数据!',
      detailHasItemRepeat: '明细中含有重复项目!'
    },
    checkText: {
      checkFormFail: '操作失败, 请检查数据后重试!',
      mustInput: '请填写'
    },
    homeHeader: {
      logout: '注销'
    },
    combobox: {
      sex: {
        male: '男',
        female: '女'
      },
      yesOrNo: {
        yes: '是',
        no: '否'
      }
    }
  },
  router: {
    sideBar: {
      homepage: '首页',
      baseModule: '基础设置',
      ownerOfCargo: '货主信息',
      menuBasicSettings: '菜单基础设置',
      userManagement: '用户管理',
      commodityCategorySetting: '商品类别设置',
      commodityManagement: '商品管理',
      userRoleSetting: '用户角色设置',
      companySetting: '公司信息',
      freightSetting: '运费设置',
      warehouseSetting: '仓库设置',
      customer: '客户信息',
      supplier: '供应商信息',
      roleMenu: '角色菜单设置'
    }
  },
  login: {
    welcomeTitle: '欢迎来到ModernWMS!👋🏻',
    mainButtonLabel: '登录',
    rememberTips: '记住账号密码',
    userName: '用户名',
    password: '密码',
    loginSuccess: '登录成功'
  },
  base: {
    freightSetting: {
      carrier: '承运商',
      departure_city: '始发城市',
      arrival_city: '到货城市',
      price_per_weight: '单公斤运费',
      price_per_volume: '单立方米运费',
      min_payment: '最小运费',
      is_valid: '是否有效'
    },
    userManagement: {
      user_num: '员工编号',
      user_name: '员工名称',
      user_role: '角色',
      contact_tel: '联系方式',
      sex: '性别',
      is_valid: '是否有效',
      restPwd: '重置密码',
      checkboxIsNull: '请选中数据!',
      beforeResetPwd: '您确定要重置这些用户的密码吗?',
      afterResetPwd: '重置密码成功!'
    },
    warehouseSetting: {
      warehouseSetting: '仓库设置',
      reservoirSetting: '库区设置',
      locationSetting: '库位设置',
      email: 'Email',
      warehouse_name: '仓库名称',
      city: '所在城市',
      address: '详细地址',
      contact_tel: '联系方式',
      manager: '负责人',
      is_valid: '是否有效',
      area_name: '库区名称',
      area_property: '库区类型',
      location_name: '库位编码',
      location_length: '库位长(m)',
      location_width: '库位宽(m)',
      location_heigth: '库位高(m)',
      location_volume: '库位容积（m³）',
      location_load: '库位承重（kg）',
      roadway_number: '巷道号',
      shelf_number: '货架号',
      layer_number: '层号',
      tag_number: '位号'
    },
    customer: {
      customer_name: '客户名称',
      city: '所在城市',
      address: '详细地址',
      manager: '负责人',
      email: 'Email',
      contact_tel: '联系方式',
      creator: '创建人',
      create_time: '创建时间',
      last_update_time: '最后更新时间',
      is_valid: '是否有效'
    },
    supplier: {
      supplier_name: '供应商名称',
      city: '所在城市',
      address: '详细地址',
      email: 'Email',
      manager: '负责人',
      contact_tel: '联系方式',
      creator: '创建人',
      create_time: '创建时间',
      last_update_time: '最后更新时间',
      is_valid: '是否有效'
    },
    userRoleSetting: {
      role_name: '用户角色',
      is_valid: '是否有效'
    },
    companySetting: {
      company_name: '公司名称',
      city: '所在城市',
      address: '详细地址',
      manager: '负责人',
      contact_tel: '联系方式'
    },
    ownerOfCargo: {
      goods_owner_name: '货主名称',
      city: '所在城市',
      address: '详细地址',
      manager: '负责人',
      contact_tel: '联系方式',
      creator: '创建人',
      create_time: '创建时间'
    },
    commodityCategorySetting: {
      category_name: '商品类别',
      parent_name: '所属类别',
      creator: '创建人',
      create_time: '创建时间'
    },
    roleMenu: {
      role_name: '角色名称',
      menu_name: '菜单名称',
      beforeUpdateOrDel: '请先选择需要处理的数据!'
    }
  }
}
