<template>
  <div class="loginForm">
    <div class="titleText">
      <h5>{{ $t('login.welcomeTitle') }}</h5>
    </div>
    <div class="formContainer">
      <v-form ref="VFormRef" v-model="data.valid" lazy-validation @keydown.enter.prevent="method.login()">
        <v-text-field
          v-model="data.userName"
          required
          :rules="data.userNameVaildRules"
          :label="$t('login.userName')"
          variant="solo"
        ></v-text-field>
        <v-text-field
          v-model="data.password"
          required
          :rules="data.passwordVaildRules"
          :autocomplete="false"
          :append-inner-icon="data.showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          :type="data.showPassword ? 'text' : 'password'"
          :label="$t('login.password')"
          variant="solo"
          @click:append-inner="method.handleShowPassword()"
        ></v-text-field>
        <v-checkbox v-model="data.remember" :label="$t('login.rememberTips')"></v-checkbox>
        <v-btn color="purple" @click="method.login()">{{ $t('login.mainButtonLabel') }}</v-btn>
      </v-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted } from 'vue'
import { Md5 } from 'ts-md5'
import i18n from '@/languages/i18n'
import { login } from '@/api/sys/login'
import { store } from '@/store'
import { hookComponent } from '@/components/system'
import { router } from '@/router/index'

// Get v-form ref
const VFormRef = ref()

const data = reactive({
  valid: true,
  showPassword: false,
  userName: '',
  password: '',
  remember: false,
  userNameVaildRules: [
    (v: string) => !!v || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('login.userName') }!`
  ],
  passwordVaildRules: [
    (v: string) => !!v || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('login.password') }!`
  ]
})

const method = reactive({
  handleShowPassword: () => {
    data.showPassword = !data.showPassword
  },
  login: async () => {
    const { valid } = await VFormRef.value.validate()
    if (!valid) {
      return
    }
    const { data: loginRes } = await login({
      user_name: data.userName,
      password: Md5.hashStr(data.password)
    })

    if (loginRes.isSuccess) {
      hookComponent.$message({
        type: 'success',
        content: i18n.global.t('login.loginSuccess')
      })
      // TODO 把用户权限信息加进来
      // let expiredTime = new Date().getTime() + loginRes.data.expire * 60 * 1000
      const expiredTime = new Date().getTime() + 1 * 60 * 1000

      store.commit('user/setToken', loginRes.data.access_token)
      store.commit('user/setRefreshToken', loginRes.data.refresh_token)
      store.commit('user/setExpirationTime', expiredTime)
      store.commit('user/setEffectiveMinutes', loginRes.data.expire)

      // test menus
      const testMenus = [
        // static
        {
          menu_name: '首页',
          module: '',
          vue_path: 'homepage',
          vue_path_detail: '',
          vue_directory: 'home/homepage'
        },
        {
          menu_name: '货主信息',
          module: 'baseModule',
          vue_path: 'ownerOfCargo',
          vue_path_detail: '',
          vue_directory: 'base/ownerOfCargo'
        },
        {
          menu_name: '用户管理',
          module: 'baseModule',
          vue_path: 'userManagement',
          vue_path_detail: '',
          vue_directory: 'base/userManagement'
        },
        {
          menu_name: '商品类别设置',
          module: 'baseModule',
          vue_path: 'commodityCategorySetting',
          vue_path_detail: '',
          vue_directory: 'base/commodityCategorySetting'
        },
        {
          menu_name: '商品管理',
          module: 'baseModule',
          vue_path: 'commodityManagement',
          vue_path_detail: '',
          vue_directory: 'base/commodityManagement'
        },
        {
          menu_name: '用户角色设置',
          module: 'baseModule',
          vue_path: 'userRoleSetting',
          vue_path_detail: '',
          vue_directory: 'base/userRoleSetting'
        },
        {
          menu_name: '公司信息',
          module: 'baseModule',
          vue_path: 'companySetting',
          vue_path_detail: '',
          vue_directory: 'base/companySetting'
        },
        {
          menu_name: '运费设置',
          module: 'baseModule',
          vue_path: 'freightSetting',
          vue_path_detail: '',
          vue_directory: 'base/freightSetting'
        },
        {
          menu_name: '客户信息',
          module: 'baseModule',
          vue_path: 'customer',
          vue_path_detail: '',
          vue_directory: 'base/customer'
        },
        {
          menu_name: '供应商信息',
          module: 'baseModule',
          vue_path: 'supplier',
          vue_path_detail: '',
          vue_directory: 'base/supplier'
        }
      ]
      store.commit('user/setUserMenuList', testMenus)

      // Remember user login info
      if (data.remember) {
        const rememberJSON = JSON.stringify({
          userName: window.btoa(data.userName),
          password: window.btoa(data.password)
        })
        localStorage.setItem('userLoginInfo', rememberJSON)
      } else {
        localStorage.setItem('userLoginInfo', '')
      }
      // Jump home
      store.commit('system/setCurrentRouterPath', 'homepage')
      router.push('home')
    }
  }
})

onMounted(() => {
  // Get remember username and password
  const rememberJSON = localStorage.getItem('userLoginInfo')
  if (rememberJSON) {
    const obj = JSON.parse(rememberJSON)
    data.userName = window.atob(obj.userName)
    data.password = window.atob(obj.password)
    data.remember = true
  }
})
</script>

<style scoped lang="less">
.loginForm {
  // min-height: ;
  height: 50%;
  width: 100%;
  box-sizing: border-box;
  padding: 16px;
  .titleText {
    box-sizing: border-box;
    padding: 20px;
    h5 {
      font-size: 1.5rem !important;
      font-weight: 500;
      line-height: 2rem;
      letter-spacing: normal !important;
      font-family: inter, sans-serif, -apple-system, blinkmacsystemfont, Segoe UI, roboto, Helvetica Neue, arial,
        sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', Segoe UI Symbol !important;
      text-transform: none !important;
    }
  }
  .formContainer {
    box-sizing: border-box;
    padding: 12px 20px;
    .v-btn {
      width: 100%;
    }
    .v-text-field {
      margin-top: 10px;
    }
    .v-checkbox {
      color: #b2b0b5;
      margin-inline-start: -0.5625rem;
      margin-top: -10px;
      height: 60px;
    }
  }
}
</style>
