<template>
    <v-layout align-start>
        <v-flex>
            <v-container v-if="verOrdenpago==0">
                <v-toolbar flat color="white">
                    <v-toolbar-title>Orden de pago</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer> 
                </v-toolbar>
                <v-data-table
                    :headers="headers"
                    :items="ordendepagos"
                    :search="search"
                    :items-per-page="15"
                    class="elevation-1">                   
                    <template v-slot:[`item.opciones`]="{ item }">
                        <td class=" justify-center layout px-0 mt-3">
                            <v-btn color="primary" @click="editItem(item)">Ver</v-btn>
                            <v-btn><v-icon @click="mostrarSolicitud(item)">print</v-icon></v-btn>
                        </td>    
                    </template>
                    <template slot="items" slot-scope="props">
                        <td>{{ props.item.idordendepago }}</td>
                        <td>{{ props.item.estado }}</td>
                        <td>{{ props.item.usuario }}</td>
                        <td>{{ props.item.regional }}</td>  
                        <td>{{ props.item.cuenta}}</td>                 
                    </template> 
                    <template v-slot:[`item.rendido`]="{ item }">
                    <div v-if="item.rendido==1">
                        <span class="blue--text">Si</span>
                    </div>
                    <div v-if="item.rendido==0">
                        <span class="red--text">No</span>
                    </div>
                    </template>
                    <template v-slot:[`item.Cambiar`]="{ item }">
                        <td class=" justify-center">
                            <v-icon color="red" @click="marcarRendido(item)">block</v-icon>
                        </td>
                    </template>
                    <template slot="no-data">
                        <v-btn color="primary" @click="listar">No hay datos</v-btn>
                    </template>
                </v-data-table>
            </v-container>
            <v-container v-if="verOrdenpago">
                <template fluid>
                    <v-btn color="primary" @click="volver">Volver</v-btn>
                    <p class="text-subtitle-2 text-center">CASO: {{id}}</p>     
                    <v-row aling="center">
                        <v-toolbar color="blue" dark>Datos Usuario</v-toolbar>     
                    </v-row>
                    <v-row aling="center" cols="6" sm="1">
                        <v-col class="d-inline-flex" >
                            <v-subheader>Usuario:</v-subheader>
                            <v-text-field v-model="nombreusuario" placeholder="Placeholder" outlined disabled ></v-text-field>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Area:</v-subheader>
                            <v-select  v-model="idarea" :items="Areas" label="Selecione un area" outlined></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Regional:</v-subheader>
                            <v-select  v-model="idregional" :items="Regionales" label="Selecione una regional" outlined></v-select>
                        </v-col>
                    </v-row>
                    <v-row aling="center" cols="6" sm="1">
                        <v-col class="d-flex">
                            <v-subheader>Empresa:</v-subheader>
                            <v-select  v-model="idempresa" :items="Empresas" label="Seleccione una empresa" outlined></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>fecha solicitud:</v-subheader>
                            <v-text-field v-model="fechasolicitada" class="text-xs-center" disabled></v-text-field>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>fechaprogramada:</v-subheader>
                            <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40"
                            transition="scale-transition" offset-y min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="fechaprogramada"  prepend-icon="mdi-calendar"
                                        readonly v-bind="attrs" v-on="on">
                                    </v-text-field>
                                </template>
                                <v-date-picker v-model="fechaprogramada" @input="menu2 = false">                                
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                    <v-row aling="center" cols="6" sm="2">
                        <v-col class="d-flex">
                            <v-subheader>Tipo de Gasto:</v-subheader>
                            <v-select  v-model="idtipogasto" :items="TipoGastos" label="Seleccione tipo de gasto" @change="Selectdinamic(idtipogasto)" outlined ></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Especificaion de gasto:</v-subheader>
                            <v-select  v-model="idespecifgasto" :items="EspecifGastos" label="Seleccione especificacion" outlined></v-select>
                        </v-col>
                    </v-row>
                    <v-row aling="center">
                        <v-toolbar color="blue" dark>Datos solicitud</v-toolbar>     
                    </v-row>
                    <v-row aling="center" >
                        <v-col class="d-flex" >
                            <v-subheader>Proyecto:</v-subheader>
                            <v-select  v-model="idproyecto" :items="Proyectos" label="Seleccione proyecto" outlined></v-select>
                        </v-col> 
                        <v-divider inset vertical></v-divider> 
                        <v-col class="d-flex" >
                            <v-subheader>Moneda:</v-subheader>
                            <v-select  v-model="idmoneda" :items="Monedas" label="Seleccione moneda" outlined></v-select>
                        </v-col>  
                        <v-divider inset vertical></v-divider>
                        <v-col class="d-flex">
                            <v-subheader>modo pago:</v-subheader>
                            <v-select  v-model="idmodopago" :items="ModoPagos" label="Seleccione modo pago" outlined></v-select>
                        </v-col>      
                    </v-row>
                    <v-row aling="center">
                        <v-col class="d-flex" cols="6" sm="4">
                            <v-subheader>tipo de solicitud:</v-subheader>
                            <v-select  v-model="idtiposolicitud" :items="TipoSolicitudes" label="Seleccione tipo de solicitud" outlined></v-select>
                        </v-col> 
                        <v-divider inset vertical></v-divider> 
                        <v-col class="d-flex" cols="6" sm="3">
                            <label class="mt-5 ml-4">Tiene:</label>
                            <v-spacer></v-spacer>
                            <v-checkbox v-model="factura" color="blue" label="Factura" row></v-checkbox> 
                            <v-spacer></v-spacer>
                            <v-checkbox v-model="recibo" color="blue" label="Recibo"></v-checkbox>   
                        </v-col>
                    </v-row>
                    <v-row aling="center">
                        <template >
                            <v-toolbar dark color="blue" class="mt-2">
                                <v-toolbar-title>Cuenta destino:</v-toolbar-title>
                                <v-autocomplete v-model="idcuenta" :items="Cuentas" 
                                    :search-input.sync="searchcuentas" cache-items
                                    class="mx-6" hide-details label="Escriba el nro de cuento o el nombre" solo-inverted>
                                </v-autocomplete>
                            </v-toolbar>   
                        </template>    
                    </v-row>
                    <v-row aling="center">
                        <v-textarea v-model="concepto" label="Concepto" auto-grow outlined
                            rows="3" row-height="25" class="mt-4"></v-textarea> 
                    </v-row>
                    <v-container>
                        <v-btn color="blue darken-1" @click="agregarDetalle">+</v-btn>
                        <v-data-table
                            :headers="cabeceraDetalles"
                            :items="detalles"
                            hide-default-footer
                            class="elevation-2">
                            <template v-slot:[`item.borrar`]="{ item }">
                                <td class=" justify-center layout px-0">
                                    <v-icon small class="mr-2" @click="eliminardetalle(detalles,item)">
                                        delete
                                    </v-icon>
                                </td>
                            </template>
                            <template v-slot:[`item.detalle`]="props">
                                <td><v-text-field v-model="props.item.detalle"></v-text-field></td>
                            </template>
                            <template v-slot:[`item.nrodocumento`]="props">
                                <td><v-text-field v-model="props.item.nrodocumento"></v-text-field></td>
                            </template>
                            <template v-slot:[`item.monto`]="props">
                                <td><v-text-field type="Number" v-model="props.item.monto"></v-text-field></td>
                            </template>
                            <template slot="no-data">
                                <strong>DEBE AGREGAR EL DETALLE</strong>
                            </template>
                        </v-data-table>
                        <v-flex class="text-right mt-2 mr-10">
                            <strong>Total:</strong>{{total=(calcularTotal).toFixed(2)}}
                        </v-flex>
                    </v-container>
                </template>
                <template>
                    <v-container class="mt-5">
                        <v-row aling="center" cols="6" sm="1">
                            <v-spacer></v-spacer>
                            <v-btn class="mt-2 mr-10" color="green darken-1" @click="cambiarContador" >Cambiar contador</v-btn>
                            <v-subheader>Contabilidad:</v-subheader>
                            
                            <v-select class="mr-10" v-model="idcontador" :items="Contadores" label="Selecione un encargado de contabilidad" outlined></v-select>
                        </v-row>
                        <v-row aling="center" cols="6" sm="2">
                            <v-spacer></v-spacer>
                            <v-subheader>Aprobador:</v-subheader>
                            <v-select class="mr-10" v-model="idaprobador" :items="Aprobadores" label="Selecione un aprobador" outlined></v-select>
                        </v-row>
                        <v-flex xs12 sm12 md12 v-show="valida">
                            <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                             </div>
                        </v-flex>
                        <v-row>
                            <v-spacer></v-spacer>
                            <v-btn class="mt-2" color="primary" @click="volver">Volver</v-btn>
                            <v-spacer></v-spacer>
                        </v-row >
                        <v-dialog v-model="dialog" max-width="500px">
                            <v-card>
                                <v-card-title>
                                <span class="headline">{{ formTitle}}</span>
                                </v-card-title>
                                <v-card-text>hola</v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn @click.native="close" rounded block elevation="10" color="blue" dark class="mb-3">Cerrar</v-btn>
                                    <v-spacer></v-spacer>
                                </v-card-actions>
                            </v-card>
                        </v-dialog>
                        <v-dialog v-model="dialogContador" max-width="500px">
                            <v-card>
                                <v-card-title>
                                <span class="headline">Se ha modificado el contador</span>
                                </v-card-title>
                                <v-card-text>
                                -------
                                </v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn  @click.native="close" rounded block elevation="10" color="blue" dark class="mb-3">Cerrar</v-btn>
                                    <v-spacer></v-spacer>
                                </v-card-actions>
                            </v-card>
                        </v-dialog>             
                    </v-container>
                </template>
                <v-flex xs12 sm12 md12 v-show="valida">
                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                    </div>
                </v-flex>
            </v-container>
            <v-dialog v-model="solicitudModal" max-width="1000px">
                <v-card>
                    <v-card-text>
                        <br><v-btn @click="crearPDF()"><v-icon>print</v-icon></v-btn>
                        <div id="solicitud">
                            <header>
                                <div id="titulo">
                                    <p>SOLICITUD DE FONDOS Y PAGOS<br></p>
                                </div>
                                <div v-if="idempresa==1">
                                    <div id="logo">
                                        <img src="data:image/png;base64,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" id="imagen">
                                    </div>
                                </div>
                                <div v-if="idempresa==2">
                                    <div id="logo">
                                        <img src="data:image/png;base64,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">
                                    </div>    
                                </div>
                                <div id="nrosolicitud">
                                    <p>Nro. Solicitud: {{id}}<br>
                                    </p>
                                </div>
                            </header>
                            <br>
                            <section>
                                <div>
                                    <table id="datossolicitud">
                                        <tbody>
                                            <tr>
                                                <td id="datosolic" style=" width: 50%; ">
                                                    <strong>Fecha de solicitud: </strong>{{fechasolicitada}}<br>
                                                    <strong>Solicitado por: </strong>{{solicitante}}<br>
                                                    <strong>Regional: </strong>{{regional}}<br>
                                                    <strong>Tipo de Gasto: </strong>{{tipodegasto}}<br>
                                                    <strong>Tipo de solicitud: </strong>{{tipodesolicitud}}<br>
                                                    <strong>Factura: </strong>{{fact}}<br>
                                                </td>
                                                <td id="datosolic">
                                                    <strong>Fecha de pago: </strong>{{fechaprogramada}}<br>
                                                    <strong>Aprobado por: </strong>{{aprobador}}<br>
                                                    <strong>Area: </strong>{{area}}<br>
                                                    <strong>Especif. de gasto: </strong> {{especifgasto}}<br>
                                                    <strong>Recibo: </strong> {{rec}}<br> 
                                                    <strong>Estado: </strong> {{estado}}<br> 
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </section>
                            <br>
                            <section>
                                <div id="cuenta">
                                    <strong>Cuenta: </strong> {{cuenta}}<br><br>
                                </div>
                            </section>

                            <section>
                                <div id="concepto">
                                    <strong>Concepto: </strong> {{concepto}}<br><br>
                                </div>
                            </section>
                            <section>
                                <div>
                                    <table id="detalle">
                                        <thead>
                                            <tr>
                                                <th style=" width: 60%;">DETALLE</th>
                                                <th>DOC.</th>
                                                <th>MONTO</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="det in detalles" :key="det.iddetalle_sol">
                                                <td style="text-align:left; width: 60%; ">{{det.detalle}}</td>
                                                <td style="text-align:left;">{{det.nrodocumento}}</td>
                                                <td style="text-align:right;">{{det.monto.toFixed(2)}}</td>
                                            </tr>
                                            
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th></th>   
                                                <th style="text-align:right; border: 1px solid black; padding: 2px;">TOTAL</th>
                                                <th style="text-align:right; border: 1px solid black; padding: 2px;">{{moneda}} {{total=(calcularTotal).toFixed(2)}}</th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </section>
                            <br>
                            <br>
                        </div>
                        <v-btn @click="ocultarSolicitud()" color="blue dark-1">Cancelar</v-btn>
                    </v-card-text>
                </v-card>
            </v-dialog>
        </v-flex>
    </v-layout>
</template>

<script>
    import axios from 'axios'
    import jsPDF from 'jspdf'
    import html2canvas from 'html2canvas';  

    export default {
        data(){
            return {
                ordendepagos1: [],
                ordendepagos: [],
                dialog: false,
                dialogContador:false,
                headers: [                    
                    { text: 'CASO', value: 'idordendepago'},
                    { text: 'Opciones', value: 'opciones'  } ,
                    { text: 'ESTADO', value: 'estado'},
                    { text: 'USUARIO', value: 'usuario'  },
                    { text: 'EMPRESA', value: 'empresa'  },
                    //{ text: 'REGIONAL', value: 'regional'  },
                    //{ text: 'AREA', value: 'area'  },
                    { text: 'APROBADOR', value: 'aprobador' },
                    //{ text: 'TIPO DE PAGO', value: 'tiposolicitud' },
                    { text: 'FECHA PROGRAMADA', value: 'fechaprogramada' },
                    { text: 'CUENTA', value: 'cuenta' },                   
                    { text: 'MONEDA', value: 'moneda' },
                    { text: 'IMPORTE', value: 'total' },
                    { text: 'RENDICION', value: 'rendido' },
                    //{ text: 'CONCEPTO', value: 'concepto' }
                    { text: 'Cambiar', value: 'Cambiar', sortable: false } 
                    ],
                detalles:[],
                cabeceraDetalles: [
                    { text: 'Borrar', value: 'borrar', sortable: false } ,
                    { text: 'Detalle', value: 'detalle' },
                    { text: 'Nro Documento', value: 'nrodocumento' },
                    { text: 'Monto', value: 'monto' },
                ],    
                search: '',
                editedIndex: -1,
                id:'',
                Areas:[],
                Regionales:[],
                Empresas:[],
                TipoGastos:[],
                EspecifGastos:[],
                TipoSolicitudes:[],
                Monedas:[],
                ModoPagos:[],
                Proyectos:[],
                EspecifGastos:[],
                Cuentas:[],
                Aprobadores:[],
                Contadores:[],
                valida: 0,
                validaMensaje:[],
                adIdTipoGasto: '',
                idusuario:'',
                nombreusuario:'',
                idarea:'',
                idregional:'',
                idempresa:'',
                idtipogasto:'',
                idespecifgasto:'',
                idtiposolicitud:'',
                idmoneda:'',
                idmodopago:'',
                idproyecto:'',
                factura: false,
                recibo: false,
                rendido:false,
                idcuenta:'',
                fechasolicitada:'',
                fechaprogramada:'',
                concepto:'',
                idaprobador:'',
                idcontador:'',                

                detalle:'',
                nrodocumento:0,
                monto:0,
                total:0,
                searchcuentas: null,
                codigobanco:'',
                idaddespgasto:'',
                diaGastos:[],
                adiddetalles:'',
                verOrdenpago:0,
                menu: false,
                modal: false,
                menu2: false,
                solicitudModal:0,

                
                
                solicitante:'',
                aprobador:'',
                tipodegasto:'',
                especifgasto:'',
                regional:'',
                area:'',
                tipodesolicitud:'',
                fact:'',
                rec:'',
                estado:'',
                cuenta:'',
                moneda:''
                



            }
        },
        computed: {
             formTitle () {
            return this.editedIndex === -1 ? 'La solicitud ha sido Aprobada' : 'La solicitud ha sido rechazada'
            },
            calcularTotal:function(){
                this.total=0;
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){
                    resultado=resultado+(this.detalles[i].monto*1);
                    //this.detalles[i].monto.push(Number(this.detalles[i].monto));
                }
                return resultado;
            }
        
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {           
            this.listar();
            this.Select();
        },
        methods:{

            listar(){
                var adidusuario=this.$store.state.usuario.idusuario;
                let me=this;
                

                axios.get('api/Ordendepagos/listar',{}).then(function(response)
                {
                    //console.log(response);
                    me.ordendepagos=response.data;
                    me.ordendepagos=me.ordendepagos.filter(od=>od.rendido == 1 && od.idestado!==5 && od.idcontador==adidusuario) //no rendido
                }).catch(function(error){
                    console.log(error);
                });
            },

            listardetalle(iddetalles){
                let me=this;
                me.detalles=[];
                this.adiddetalles=iddetalles;
                axios.get('api/Detalleordenes/Listar/'+this.adiddetalles,{}).then(function(response)
                {
                    //console.log(response);
                    me.detalles=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

           Select(){
                let me=this;
                var AreasArray=[];
                axios.get('api/Areas/Select').then(function(response)
                {
                    //console.log(response);
                    AreasArray=response.data;
                    AreasArray.map(function(x){
                        me.Areas.push({text: x.nombre,value:x.idarea});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var RegionalesArray=[];
                axios.get('api/Regionales/Select').then(function(response)
                {
                    //console.log(response);
                    RegionalesArray=response.data;
                    RegionalesArray.map(function(x){
                        me.Regionales.push({text: x.nombre,value:x.idregional});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var EmpresasArray=[];
                axios.get('api/Empresas/Select').then(function(response)
                {
                    //console.log(response);
                    EmpresasArray=response.data;
                    EmpresasArray.map(function(x){
                        me.Empresas.push({text: x.nombre,value:x.idempresa});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var MonedasArray=[];
                axios.get('api/Monedas/Select').then(function(response)
                {
                    //console.log(response);
                    MonedasArray=response.data;
                    MonedasArray.map(function(x){
                        me.Monedas.push({text: x.nombre,value:x.idmoneda});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var ModoPagosArray=[];
                axios.get('api/Modopagos/Select').then(function(response)
                {
                    //console.log(response);
                    ModoPagosArray=response.data;
                    ModoPagosArray.map(function(x){
                        me.ModoPagos.push({text: x.nombre,value:x.idmodopago});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var ProyectosArray=[];
                axios.get('api/Proyectos/Select').then(function(response)
                {
                    //console.log(response);
                    ProyectosArray=response.data;
                    ProyectosArray.map(function(x){
                        me.Proyectos.push({text: x.nombre,value:x.idproyecto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoSolicitudesArray=[];
                axios.get('api/Tiposolicitudes/Select').then(function(response)
                {
                    //console.log(response);
                    TipoSolicitudesArray=response.data;
                    TipoSolicitudesArray.map(function(x){
                        me.TipoSolicitudes.push({text: x.nombre,value:x.idtiposolicitud});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoGastosArray=[];
                axios.get('api/TipoGastos/Select').then(function(response)
                {
                    //console.log(response);
                    TipoGastosArray=response.data;
                    TipoGastosArray.map(function(x){
                        me.TipoGastos.push({text: x.nombre,value:x.idtipogasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var CuentasArray=[];
         
                axios.get('api/Cuentas/Select').then(function(response)
                {
                    //console.log(response);
                    CuentasArray=response.data;
                    CuentasArray.map(function(x){
                        me.Cuentas.push({text: x.cuenta+' - '+x.nombre+' - '+x.banco,value:x.idcuenta});
                    });
                    //me.codigobanco=Cuentas.text2;
                }).catch(function(error){
                    console.log(error);
                });

                var AprobadoresArray=[];
         
                axios.get('api/Usuarios/Selectaprobador').then(function(response)
                {
                    //console.log(response);
                    AprobadoresArray=response.data;
                    AprobadoresArray.map(function(x){
                        me.Aprobadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                var ContadoresArray=[];        
                axios.get('api/Usuarios/Selectcontador').then(function(response)
                {
                    //console.log(response);
                    ContadoresArray=response.data;
                    ContadoresArray.map(function(x){
                        me.Contadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                     
            },

            Selectdinamic(idtipogasto){
                let me=this;
                me.idespecifgasto='';
                me.EspecifGastos=[];
                this.adIdTipoGasto=idtipogasto;
                var EspecifGastosArray=[];
                axios.get('api/Especifgastos/Select/'+this.adIdTipoGasto,{}).then(function(response)
                { 
                    //console.log(response);
                    EspecifGastosArray=response.data;
                    EspecifGastosArray.map(function(x){
                        me.EspecifGastos.push({text: x.nombre,value:x.idespecifgasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },

           agregarDetalle(){
               this.detalles.push(
                   {detalle: "",nrodocumento:"",monto:0
                   }
               );
            },

            eliminardetalle(arr,item){
                var i=arr.indexOf(item);
                if(i!==-1){
                    arr.splice(i,1)
                }
            },

            volver(){
                this.verOrdenpago=0;
            },

            convertirnumero(){
                for(var i=0;i<this.detalles.length;i++){
                  this.detalles[i].monto=(this.detalles[i].monto)*1;
                }
            },

           
            editItem (item) {
                this.verOrdenpago=1;
                this.id=item.idordendepago;
                this.idusuario=item.idusuario;
                this.nombreusuario=item.usuario;
                this.idarea=item.idarea;
                this.idregional=item.idregional;
                this.idempresa=item.idempresa;
                this.fechasolicitada=item.fechasolicitud;
                this.fechaprogramada=item.fechaprogramada
                this.idtipogasto=item.idtipogasto;
                this.idespecifgasto=item.idespecifgasto;
                this.idtiposolicitud=item.idtiposolicitud;
                this.idmoneda=item.idmoneda;
                this.idmodopago=item.idmodopago;
                this.idproyecto=item.idproyecto;
                this.factura=item.factura;
                this.recibo=item.recibo;
                this.rendido=item.rendido;
                this.editedIndex=1;
                this.concepto=item.concepto;
                this.idcuenta=item.idcuenta;
                this.idaprobador=item.idaprobador;
                this.idcontador=item.idcontador;
                let me=this;
                this.Selectdinamic(me.idtipogasto);
                this.idespecifgasto=item.idespecifgasto;
                this.listardetalle(me.id);
            },

            close () {
                this.dialogContador=false;
                this.dialog = false;
                this.volver();
                this.listar();
                this.editedIndex=-1;
            },  

            marcarRendido(item){
                let me=this;
                me.adId=item.idordendepago;
                axios.put('api/Ordendepagos/Rendido',{
                    'idordendepago':me.adId,
                    'rendido':false,
                }).then(function(response){            
                    me.adId="";     
                    me.listar();            
                 }).catch(function(error){
                    console.log(error);
                });
            },

            cambiarContador(){
                let me=this;
                axios.put('api/Ordendepagos/Cambiarcontador',{
                        'idordendepago':me.id,
                        'idcontador':me.idcontador,
                    }).then(function(response){            
                        me.dialogContador=true;
                        me.validaMensaje=[];        
                    }).catch(function(error){
                        console.log(error);
                    })
            },
            
            mostrarSolicitud(item){
                this.id=item.idordendepago;
                this.fechasolicitada=item.fechasolicitud;
                this.fechaprogramada=item.fechaprogramada;
                this.solicitante=item.usuario;
                this.aprobador=item.aprobador;
                this.tipodegasto=item.nombretipogasto;
                this.especifgasto=item.especifgasto;
                this.regional=item.regional;
                this.area=item.area;
                this.tipodesolicitud=item.tiposolicitud;
                this.estado=item.estado;
                this.cuenta=item.cuenta;
                this.concepto=item.concepto;
                this.idempresa=item.idempresa;
                this.moneda=item.moneda;
                //this.total=item.total;
                if(item.factura){
                    this.fact="SI";
                }else
                {
                    this.fact="NO";
                }
                if(item.recibo){
                    this.rec="SI";
                }else
                {
                    this.rec="NO";
                }
                this.listardetalle(item.idordendepago)


                this.solicitudModal=1;
            },
            ocultarSolicitud(){
                this.solicitudModal=0;
            },

            crearPDF(){
                var quotes = document.getElementById('solicitud');
                html2canvas(quotes).then(function (canvas) {
                    var imgData = canvas.toDataURL('image/png');
                    var imgWidth = 210;
                    var pageHeigth = 295;
                    var imgHeigth = canvas.height * imgWidth / canvas.width;
                    var heightLeft = imgHeigth;
                    var doc = new jsPDF('p', 'mm','a4');
                     var position = 0;
                     doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeigth);
                     doc.save('Solicituddefondos.pdf');
             });
            }
        }        
    }
</script>
<style>
    #solicitud {
        padding: 20px;
        font-family: Arial, sans-serif;
        font-size: 16px ;
    }
    #titulo {
        float: center;
        font-size: 22px;
        text-align: center;
        font-weight: bold;
    }
    #logo {
        float: center;
        margin-left: 2%;
        margin-right: 2%;
        text-align: center;
    }
    #imagen {
        width: 120px;
    }

    #nrosolicitud {
        font-size: 18px;
        font-weight: bold;
        text-align: right;
        margin-right: 2%;
    }   

    #datos {
        float: left;
        margin-top: 0%;
        margin-left: 2%;
        margin-right: 2%;
        /*text-align: justify;*/
    }

    #encabezado {
        text-align: center;
        margin-left: 10px;
        margin-right: 10px;
        font-size: 16px;
    }

    section {
        clear: left;
    }

    #datosolic {
        text-align: left;
        margin-left: 20px;
    }
    #datosolic2 {
        text-align: left;
        margin-left: 20px;
    }

    #concepto {
        text-align: left;
        font-size: 16px;
    }
    #cuenta {
        text-align: left;
        font-size: 16px;
    }

    #datossolicitud {
        width: 100%;
        border-collapse: collapse;
        border-spacing: 0;
        margin-bottom: 15px;
    }

    #de {
        font-size: 14px;
    }

    #detalle {
        width: 100%;
        border-collapse: collapse;
        border-spacing: 1px;
        padding: 20px;
        margin-bottom: 15px;
    }

    #detalle thead tr th {
        border: 1px solid black;
        padding: 2px;
        text-align: center; 
    }

    #detalle tbody tr td {
        border: 1px solid black;
        padding: 2px;     
    }
</style>