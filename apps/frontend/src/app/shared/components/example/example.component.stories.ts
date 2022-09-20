import {Meta, moduleMetadata, Story} from '@storybook/angular';
import {ExampleComponent} from './example.component';
import {PrimengModule} from "../../primeng/primeng.module";

export default {
  title: 'ExampleComponent',
  component: ExampleComponent,
  decorators: [
    moduleMetadata({
      imports: [PrimengModule],
    })
  ],
} as Meta<ExampleComponent>;

const Template: Story<ExampleComponent> = (args: ExampleComponent) => ({
  props: args,
});


export const Primary = Template.bind({});
Primary.args = {}
