export default {
  system: {
    hookComponent: {
      dialog: {
        defaultClose: 'Close',
        defaultConfirm: 'Agree',
        defaultTitle: 'Tips'
      }
    },
    page: {
      add: 'Add',
      update: 'Update',
      refresh: 'Refresh',
      export: 'Export',
      operate: 'Operate',
      edit: 'Edit',
      delete: 'Delete',
      close: 'Colse',
      submit: 'Submit'
    },
    tips: {
      success: ' success!',
      fail: ' fail!',
      beforeDeleteMessage: 'Do you want to delete this data?',
      beforeDeleteDetailMessage: 'Are you sure you want to delete this row?',
      detailLengthIsZero: 'Details verification before document submission!',
      detailHasItemRepeat: 'The details contain duplicate items!',
      noData: 'no data'
    },
    checkText: {
      checkFormFail: 'Operation failed. Please check the data and try again!',
      mustInput: 'Please fill in the '
    },
    homeHeader: {
      logout: 'Logout'
    },
    combobox: {
      sex: {
        male: 'Male',
        female: 'Female'
      },
      yesOrNo: {
        yes: 'yes',
        no: 'no'
      }
    }
  },
  router: {
    sideBar: {
      homepage: 'Home Page',
      baseModule: 'Basic Settings',
      ownerOfCargo: 'Owner Information',
      menuBasicSettings: 'Menu',
      userManagement: 'User Management',
      commodityCategorySetting: 'Commodity Category',
      commodityManagement: 'Commodity Management',
      userRoleSetting: 'User Role',
      companySetting: 'Company Information',
      freightSetting: 'Freight Setting',
      warehouseSetting: 'Warehouse Setting',
      customer: 'Customer Info',
      supplier: 'Supplier Info',
      roleMenu: 'Role Menu Settings'
    }
  },
  login: {
    welcomeTitle: 'Welcome to ModernWMS!👋🏻',
    mainButtonLabel: 'Login',
    rememberTips: 'Remember me',
    userName: 'UserName',
    password: 'Password',
    loginSuccess: 'Login succeeded!',
    notAuthority: 'Your account does not contain any menu, please contact the administrator to add it!'
  },
  base: {
    freightSetting: {
      carrier: 'carrier',
      departure_city: 'Departure City',
      arrival_city: 'Arrival City',
      price_per_weight: 'Weight Fee',
      price_per_volume: 'Volume Fee',
      min_payment: 'Min Payment'
    },
    userManagement: {
      user_num: 'User Num',
      user_name: 'User Name',
      user_role: 'Role',
      contact_tel: 'Contact Information',
      sex: 'Sex',
      is_valid: 'Valid',
      restPwd: 'Reset password',
      checkboxIsNull: 'Please select data!',
      beforeResetPwd: 'Are you sure you want to reset the passwords of these users?',
      afterResetPwd: 'Password reset succeeded!'
    },
    warehouseSetting: {
      warehouseSetting: 'Warehouse Setting',
      reservoirSetting: 'Reservoir Setting',
      locationSetting: 'Location Setting',
      email: 'Email',
      warehouse_name: 'Warehouse Name',
      city: 'City',
      address: 'Address',
      contact_tel: 'Acontact Tel',
      manager: 'Manager',
      is_valid: 'Valid',
      area_name: 'Reservoir Name',
      area_property: 'Reservoir Category',
      location_name: 'Location Coding',
      location_length: 'Location Length(m)',
      location_width: 'Location Width(m)',
      location_heigth: 'Location Height(m)',
      location_volume: 'Location Volume（m³）',
      location_load: 'Location Load（kg）',
      roadway_number: 'Roadway Number',
      shelf_number: 'Shelf Number',
      layer_number: 'Layer Number',
      tag_number: 'Tag Number',
      picking_area: 'Picking Area',
      stocking_area: 'Stocking Area',
      receiving_area: 'Receiving Area',
      return_area: 'Return Area',
      defective_area: 'Defective Area',
      inventory_area: 'Inventory Area'
    },
    customer: {
      customer_name: 'Customer Name',
      city: 'City',
      address: 'Address',
      manager: 'Manager',
      email: 'Email',
      contact_tel: 'Contact Tel',
      creator: 'Creater',
      create_time: 'Create Time',
      last_update_time: 'Last Update Time',
      is_valid: 'Valid'
    },
    supplier: {
      supplier_name: 'Supplier Name',
      city: 'City',
      address: 'Address',
      email: 'Email',
      manager: 'Manager',
      contact_tel: 'Contact Tel',
      creator: 'Creator',
      create_time: 'Create Time',
      last_update_time: 'Last Update Time',
      is_valid: 'Valid'
    },
    userRoleSetting: {
      role_name: 'User Role',
      is_valid: 'Valid'
    },
    companySetting: {
      company_name: 'Corporate Name',
      city: 'City',
      address: 'Detailed Address',
      manager: 'Person In Charge',
      contact_tel: 'Contact Information'
    },
    ownerOfCargo: {
      goods_owner_name: 'Owner Of Cargo',
      city: 'City',
      address: 'Detailed Address',
      manager: 'Person In Charge',
      contact_tel: 'Contact Information',
      creator: 'Creater',
      create_time: 'Creation Time'
    },
    commodityCategorySetting: {
      category_name: 'Commodity category',
      parent_name: 'Parent Category',
      creator: 'Creater',
      create_time: 'Creation Time'
    },
    roleMenu: {
      role_name: 'Role Name',
      menu_name: 'Menu Name',
      beforeUpdateOrDel: 'Please select the data to be processed first!'
    }
  }
}
