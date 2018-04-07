<template>
  <div class="component-card card">
    <div class="p-5 card__body">
      <h4 class="card__title">
        <nuxt-link :to="link" class="c-black" v-html="title"></nuxt-link>
      </h4>
      <div class="card__subtitle c-dark-grey mb-5" v-html="subtitle"></div>
      <div class="card__text" v-html="text"></div>
    </div>
  </div>
</template>

<script type="text/javascript">
  export default {
    name: "Card",

    props: {
      link: {
          type: String,
          default: ''
      },
      subtitle: {
        type: String,
        default: ''
      },
      text: {
        type: String,
        default: ''
      },
      title: {
        type: String,
        default: ''
      }
    }
  }
</script>

<style lang="sass">
  .component-card
    height: 100%
    .card__title
      a
        text-decoration: none
</style>