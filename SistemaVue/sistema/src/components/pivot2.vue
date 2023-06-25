<template>
    <v-container>
        <vue-pivottable-ui
        :data="pivotData"
        :rows="rows"
        :cols="cols"
        :vals="vals"
        :aggregatorName="aggregatorName" 
        ></vue-pivottable-ui>  
    </v-container>
    
</template>

  
<script>
    import axios from 'axios'
    import { VuePivottableUi} from 'vue-pivottable'
    import 'vue-pivottable/dist/vue-pivottable.css'
    export default {
        components: {
            VuePivottableUi,
        },
        data(){
            return {
                aggregatorName: 'Sum',
                pivotData: [],
                aggregatorName: 'Sum',
                rendererName: 'Table Heatmap',
                rows: ['especifgasto'],
                cols: ['fechaprogramada'],
                vals: ['total']
            }
        },

        created () {          
            this.listar();
        },
        methods:{
            listar(){
                let me=this;
                axios.get('api/ordendepagos/listar').then(function(response)
                {
                    console.log(response);
                    me.pivotData=response.data;

                }).catch(function(error){
                    console.log(error);
                });
            },

        },
    }
</script>
