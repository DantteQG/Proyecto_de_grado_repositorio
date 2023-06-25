<template>
        <vue-pivottable
        :data="pivotData"
        :rows="rows"
        :cols="cols"
        :vals="vals"
        :aggregatorName="aggregatorName" 
        ></vue-pivottable>  
 
</template>

  
<script>
import axios from 'axios'
import { VuePivottable} from 'vue-pivottable'
import 'vue-pivottable/dist/vue-pivottable.css'
export default {
components: {
    VuePivottable,
},
data(){
    return {
        aggregatorName: 'Sum',
        pivotData: [],
        aggregatorName: 'Sum',
        rendererName: 'Table Heatmap',
        rows: ['especifgasto'],
        cols: ['fechapromada'],
        vals: ['total']
    }
},

created () {          
    this.listar();
},
methods:{
    listar(){
            let me=this;
            axios.get('api/ordendepagos/pivot').then(function(response)
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
