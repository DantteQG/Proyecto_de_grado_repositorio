<template>
  <v-app id="inspire">
    <v-navigation-drawer
      v-model="drawer"
      app
      v-if="logueado">

      <v-list dense>
        <template>
          <v-btn  :to="{ name: 'nuevasolicitudes'}" rounded block elevation="10" color="blue" dark class="mb-3">Nuevo</v-btn>
        </template>
        <template v-if="esAdministrador || esIniciador || esAprobador || esContador || esCreadorcuenta || esTesoreria">
          <!-- <v-list-item :to="{ name: 'home'}">
            <v-list-item-action>
              <v-icon>home</v-icon>
            </v-list-item-action>
            <v-list-item-title>
              Inicio
            </v-list-item-title>
          </v-list-item> -->

          <template v-if="esAdministrador || esTesoreria">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  REPORTES
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          <v-list-item :to="{ name: 'pivot'}">
              <v-list-item-action>
                <v-icon>home</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Reporte flujo de pagos dia
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            
            <v-list-item :to="{ name: 'pivot3'}" >
              <v-list-item-action>
                <v-icon>home</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Reporte BI
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>    
           </v-list-group>
            </template>    
        </template>
        
        <template v-if="esAdministrador">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  ADMINISTRACION
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'areas'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Areas
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'empresas'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Empresas
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'especifgastos'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Especificacion de gastos
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'estados'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Estado
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'modopagos'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Modo de pago
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'monedas'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Monedas
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'proyectos'}">
              <v-list-item-action>
                <v-icon>linear_scale</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Proyectos
                </v-list-item-title>  
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'regionales'}">
              <v-list-item-action>
                <v-icon>linear_scale</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Regionales
                </v-list-item-title>  
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'tipogastos'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Tipos de gastos
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'tiposolicitudes'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Tipo de solicitud
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
        <template v-if="esAdministrador">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  USUARIOS
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'roles'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Roles
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'usuarios'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Usuarios
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
        <template v-if="esAdministrador || esCreadorcuenta || esTesoreria">
            <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  CUENTAS
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'bancos'}" v-if="esAdministrador || esTesoreria">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Bancos
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'cuentas'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Cuentas
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'cuentasalidas'}" v-if="esAdministrador || esTesoreria">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Cuentas salida
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
        <template v-if="esAdministrador || esIniciador || esAprobador || esContador || esCreadorcuenta || esTesoreria">
            <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  SOLICITUDES
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'missolicitudes'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Mis solicitudes
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'poraprobar'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Por aprobar
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'enviadosparticipados'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Enviados y participados
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
        <template v-if="esAdministrador || esTesoreria">
            <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  TESORERIA
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'ordendepagos'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Todas las solicitudes
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'porcargar'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Por Cargar
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'porconfirmar'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Por Confirmar
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
        <template v-if="esAdministrador || esContador || esTesoreria">
            <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>
                  CONTABILIDAD
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item :to="{ name: 'ordendepagos'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Todas las solicitudes
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item v-if="esContador" :to="{ name: 'porrendir'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Por rendir
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item v-if="esContador" :to="{ name: 'rendido'}">
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Rendido
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
      </v-list>
      <!--  -->
    </v-navigation-drawer>

    <v-app-bar app>   
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <span class="hidden-sm-and-down">Sistema de solicitud de fondos y pagos</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <b>{{ usu }}
        <br>{{ rol }}</b>
      <v-spacer></v-spacer>
      
      
      <div >
        <v-btn @click="salir" v-if="logueado"  icon>
          <v-icon>logout</v-icon>Salir
        </v-btn>
      </div>
      <!-- <v-btn :to="{name: 'login'}" v-else>
        <v-icon>apps</v-icon>Login
      </v-btn> -->
    </v-app-bar>

    <v-main>

      
      <!-- PRINCIPAL-->
      <v-container fluid>

      <!-- If using vue-router -->
        <router-view></router-view>

      
      </v-container>
      
    

    </v-main>

    <template>
      <v-footer padless>
        <v-col
          class="text-center"
          cols="12"
        >
          {{ new Date().getFullYear() }} — <strong>PROESA</strong>
        </v-col>
      </v-footer>
    </template>

  </v-app>
</template>

<script>
  import login from '@/components/Login.vue';
  export default {
    name: 'App',
    components:{
      login
    },
    data (){
      return {
        drawer: true,
        usu : null,
        rol:null
      }
    },
    computed:{
      logueado(){
        return this.$store.state.usuario;
      },
      esAdministrador(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Administrador";},
      esIniciador(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Iniciador";},
      esAprobador(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Aprobador";},
      esCreadorcuenta(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Creador cuenta";},
      esContador(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Contador";},
      esTesoreria(){
        return this.$store.state.usuario && this.$store.state.usuario.rol == "Tesoreria";},
      

    },
    //mounted(){
      //this.mostrarUsu();
    //},

    created(){
      this.$store.dispatch("autoLogin");
      this.mostrarUsu();
    },
    methods:{
      salir(){
        this.$store.dispatch("salir")
      },
      mostrarUsu(){
        this.usu=this.$store.state.usuario.nombre;
        this.rol=this.$store.state.usuario.rol;}

    }
  }
</script>