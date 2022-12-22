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
      beforeDeleteMessage: 'Are you sure you want to delete this data?'
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
    loginSuccess: 'Login succeeded!'
  },
  base: {
    freightSetting: {
      transportationSupplier: 'Transportation Supplier',
      sendCity: 'Send City',
      receiverCity: 'Receiver City',
      weightFee: 'Weight Fee',
      volumeFee: 'Volume Fee',
      minPayment: 'Min Payment'
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
      warehouse_name: 'Warehouse Name',
      city: 'City',
      address: 'Address',
      acontact_tel: 'Acontact Tel',
      manager: 'Manager'
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
