<template>
  <div class="home" style="margin-top: 50px">
    <b-container fluid>
      <b-row>
        <b-col style="margin-left: 50px">
          <Selection/>
        </b-col>
        <b-col cols="7" style="margin-right: 50px">
          <Map/>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
// @ is an alias to /src
import Map from '@/components/Map.vue'
import Selection from '@/components/Selection.vue'

export default {
  components: {
    Map,
    Selection
  }
}
</script>
